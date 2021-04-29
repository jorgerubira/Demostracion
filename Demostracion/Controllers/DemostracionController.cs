using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Demostracion.Models;
using Demostracion.ViewData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;

namespace Demostracion.Controllers
{
    public class DemostracionController : Controller
    {
        private readonly Contexto _context;
        private readonly ServicioSQL _sql;

        public DemostracionController(Contexto context, ServicioSQL _sql)
        {
            this._context = context;
            this._sql = _sql;
        }

        public IActionResult Index()
        {
            ViewBag.Provincias = new SelectList(_context.Provincias, "id", "provincia");
            ViewBag.Hospitales = new SelectList(_context.Hospitales, "id", "hospital");
            return View();
        }

        [HttpPost]
        public IActionResult Index(DemostracionParam par)
        {
            ViewBag.Provincias = new SelectList(_context.Provincias, "id", "provincia", par.ProvinciaId);
            ViewBag.Hospitales = new SelectList(_context.Hospitales, "id", "hospital", par.HospitalId);

            string sql = " SELECT h.hospital, AVG(c.camas_ocupadas_total) " +
                          " FROM cov_h_camas c " +
                          " INNER JOIN cov_m_hospitales h ON(c.id_hospital = h.id) " +
                          " WHERE (@HospitalId=0 or @HospitalId=h.id)" +
                          " AND (@ProvinciaId=0 or h.id_municipio in ( " +
                                    " select id from cov_m_municipios where @ProvinciaId = id_provincia" +
                          " ) ) " +
                          " GROUP BY h.hospital";

            //Opcion 2
            /*string sql = " SELECT h.hospital, AVG(c.camas_ocupadas_total) " +
                          " FROM cov_h_camas c " +
                          " INNER JOIN cov_m_hospitales h ON(c.id_hospital = h.id) " +
                          " WHERE 1=1 " +
                          (par.HospitalId>0 ? " AND (@HospitalId=0 or @HospitalId=h.id)":"") +
                          (par.ProvinciaId>0 ? " AND (h.id_municipio in ( select id from cov_m_municipios where @ProvinciaId = id_provincia ) ) ":"") +
                          " GROUP BY h.hospital";*/

            MySqlParameter[] parametros =
            {
                new MySqlParameter("@ProvinciaId", par.ProvinciaId),
                new MySqlParameter("@HospitalId", par.HospitalId),
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

            return View(lista);
        }

        

    }
}
