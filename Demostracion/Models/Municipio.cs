using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demostracion.Models
{
    [Table("cov_m_municipios")]
    public class Municipio
    {
        public int id { get; set; }
        public string municipio { get; set; }

        [Column("id_provincia")]
        public int? provinciaId { get; set; }
        public Provincia provincia { get; set; }

    }
}
