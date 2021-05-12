using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demostracion.Models;
using Demostracion.ViewData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;

namespace Demostracion.Controllers
{
    public class AjaxController : Controller
    {
        private readonly Contexto _context;
        private readonly ServicioSQL _sql;

        public AjaxController(Contexto context, ServicioSQL sql)
        {
            _context = context;
            _sql = sql;
        }

        public IActionResult Index()
        {
            ViewBag.Provincias = new SelectList(_context.Provincias, "id", "provincia");

            return View();
        }

        public IActionResult Listado1(int ProvinciaId = 0, int HospitalId = 0)
        {
            string sql = " SELECT h.hospital, AVG(c.camas_ocupadas_total) " +
                          " FROM cov_h_camas c " +
                          " INNER JOIN cov_m_hospitales h ON(c.id_hospital = h.id) " +
                          " WHERE (@HospitalId=0 or @HospitalId=h.id)" +
                          " AND (@ProvinciaId=0 or h.id_municipio in ( " +
                                    " select id from cov_m_municipios where @ProvinciaId = id_provincia" +
                          " ) ) " +
                          " GROUP BY h.hospital";

            MySqlParameter[] parametros =
            {
                new MySqlParameter("@ProvinciaId", ProvinciaId),
                new MySqlParameter("@HospitalId", HospitalId),
            };

            List<DemostracionViewData> lista = _sql.EjecutarSQL<DemostracionViewData>(
                    _context,
                    sql,
                    x => new DemostracionViewData()
                    {
                        Hospital = x.GetString(0),
                        Camas = x.GetInt32(1)
                    },
                    parametros
                );

            return PartialView(lista);

        }
    }
}