using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demostracion.Models
{
    [Table("Notas")]
    public class Nota
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Valor { get; set; }

        [Column("id_alumno")]
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }

    }
}
