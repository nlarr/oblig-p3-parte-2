using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ObligatorioP3.Models
{
    public class ObliEmprendimientosContext : DbContext
    {
        public DbSet<Emprendimiento> Emprendimientos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public ObliEmprendimientosContext() : base("obligatorio") { }
    }
}