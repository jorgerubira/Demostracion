using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demostracion.Models
{
    [Table("Alumnos")]
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
