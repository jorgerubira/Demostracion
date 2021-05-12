using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demostracion.Models;
using Demostracion.ViewData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace Demostracion.Controllers
{
    public class ExcelController : Controller
    {
        private readonly Contexto _context;
        private readonly ServicioSQL _sql;

        public ExcelController(Contexto context, ServicioSQL sql)
        {
            _context = context;
            _sql = sql;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PruebaViewBag()
        {
            ViewBag.Dato = 2;
            ViewBag.Hospitales = new SelectList(_context.Hospitales, "id", "hospital");
            return View(new DemostracionViewData());
        }



        public IActionResult Generar()
        {
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            string sql = " SELECT h.hospital, AVG(c.camas_ocupadas_total) " +
                          " FROM cov_h_camas c " +
                          " INNER JOIN cov_m_hospitales h ON(c.id_hospital = h.id) " +
                          " GROUP BY h.hospital";
            
            List<DemostracionViewData> lista = _sql.EjecutarSQL<DemostracionViewData>(
                    _context, sql,
                    x => new DemostracionViewData()
                    {
                        Hospital = x.GetString(0),
                        Camas = x.GetInt32(1)
                    }
                );

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var libro = new ExcelPackage())
            {
                var worksheet = libro.Workbook.Worksheets.Add("Hospitales");
                worksheet.Cells["A1"].LoadFromCollection(lista);
                //worksheet.Cells["A1"].LoadFromCollection(lista, PrintHeaders: true);
                /*for (var col = 1; col < lista.Count + 1; col++)
                {
                    worksheet.Column(col).AutoFit();
                }*/
                /*
                var tabla = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: lista.Count + 1, toColumn: 5), "Lista");
                tabla.ShowHeader = true;
                tabla.TableStyle = TableStyles.Light6;
                tabla.ShowTotal = true;
                */

                return File(libro.GetAsByteArray(), excelContentType, "Hospitales.xlsx");
            }
        }
    }
}
