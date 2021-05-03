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
    public class MiListadoController : Controller
    {

        private readonly Contexto _context;
        private readonly ServicioSQL _sql;

        public MiListadoController(Contexto context, ServicioSQL sql)
        {
            _context = context;
            _sql = sql;
        }

        public IActionResult Index()
        {
            ViewBag.Municipios = new SelectList(_context.Municipios,
                                                "id", "municipio");
            return View();
        }


        [HttpPost]
        public IActionResult Index(int? anio, int? municipio)
        {

            string sql= " SELECT h.id, h.hospital, ifnull(MAX(camas_uci_ocupadas),0) " +
                        " FROM cov_h_camas c "+
                        " INNER JOIN cov_m_hospitales h ON(c.id_hospital= h.id) " +
                        " WHERE 1=1 " +
                        (anio!=null ? " AND year(fecha) = @anio " : "")  +
                        (municipio != null && municipio!=0 ? " AND h.id_municipio = @municipio ": "") +
                        " GROUP BY h.id, h.hospital ";


            MySqlParameter[] param =
            {
                new MySqlParameter("@anio", anio),
                new MySqlParameter("@municipio", municipio),
            };

            List<MiListadoViewData> lista= _sql.EjecutarSQL<MiListadoViewData>(
                        _context, sql,
                        x => new MiListadoViewData()
                        {
                            id = x.GetInt32(0),
                            hospital = x.GetString(1),
                            camas = x.GetInt32(2)
                        },
                        param
                    );


            ViewBag.Municipios = new SelectList(_context.Municipios, 
                                                "id", "municipio", municipio);
            ViewBag.Anio = anio;

            return View(lista);
        }



    }
}