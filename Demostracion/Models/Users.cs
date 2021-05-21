using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demostracion.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Pass { get; set; }
        public bool Activo { get; set; }
    }
}
