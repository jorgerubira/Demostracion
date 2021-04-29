using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demostracion.Models
{
    [Table("cov_m_provincias")]
    public class Provincia
    {
        public int id { get; set; }
        public string provincia { get; set; }
    }
}
