using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demostracion.Models
{
    public class UsersRoles
    {
        public int ID { get; set; }
        public string Rol { get; set; }

        [Column("id_user")]
        public int UsersId { get; set; }
        public Users Users { get; set; }
    }
}
