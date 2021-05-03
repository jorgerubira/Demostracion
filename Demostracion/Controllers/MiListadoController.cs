using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demostracion.Models;
using Demostracion.ViewData;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index(int? anio)
        {

            string sql= " SELECT h.id, h.hospital, ifnull(MAX(camas_uci_ocupadas),0) " +
                        " FROM cov_h_camas c "+
                        " INNER JOIN cov_m_hospitales h ON(c.id_hospital= h.id) " +
                        (anio!=null ? " WHERE year(fecha) = @anio " : "")  +
                        " GROUP BY h.id, h.hospital ";

            MySqlParameter[] param =
            {
                new MySqlParameter("@anio", anio)
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



            return View(lista);
        }



    }
}