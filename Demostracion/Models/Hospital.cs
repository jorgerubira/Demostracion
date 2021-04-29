using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demostracion.Models
{
    [Table("cov_m_hospitales")]
    public class Hospital
    {
        public int id { get; set; }
        public string hospital { get; set; }

        [Column("id_municipio")]
        public int? municipioId { get; set; }
        public Municipio municipio { get; set; }
    }
}
