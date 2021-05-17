using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demostracion.Models
{
    [Table("Trabajos")]
    public class Trabajo
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }

        [Column("id_alumno")]
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }
    }
}
