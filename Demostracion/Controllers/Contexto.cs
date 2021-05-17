using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Demostracion.Models
{
    public class Contexto : DbContext
    {
        public Contexto([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Camas> Camas { get; set; }
        public virtual DbSet<Hospital> Hospitales { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<Provincia> Provincias { get; set; }

        public virtual DbSet<Alumno> Alumnos { get; set; }
        public virtual DbSet<Nota> Notas { get; set; }
        public virtual DbSet<Trabajo> Trabajos { get; set; }

    }
}
