using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demostracion.Models
{
    [Table("cov_h_camas")]
    public class Camas
    {
        public int id { get; set; }
        public DateTime? fecha { get; set; }
        public int? camas_ocupadas { get; set; }
        public int? camas_uci_ocupadas { get; set; }
        public int? codigo_ine { get; set; }
        public string observaciones { get; set; }
        public int? hospitalId { get; set; }
        public Hospital hospital { get; set; }

    }
}

