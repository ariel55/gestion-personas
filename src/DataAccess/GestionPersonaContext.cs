using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;

namespace DataAccess
{
    public class GestionPersonaContext : DbContext
    {
        public DbSet<Persona> Personas { get; set; }

        public GestionPersonaContext()
        {

        }
        public GestionPersonaContext(DbContextOptions<GestionPersonaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //EFCore por defecto pluraliza las tablas. Con esto deshabilitamos esta opción
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }

            //builder.Entity<Persona>().HasKey(p => new {p.TipoDocumento, p.NroDocumento });
            //builder.Entity<Persona>().HasOne<Pais>(s => s.Pais).HasForeign(s => s.IdPais);

            // Datos de prueba
            builder.Entity<Pais>().HasData(
                new Pais { Id = 1, Nombre = "Argentina" },
                new Pais { Id = 2, Nombre = "Chile"     },
                new Pais { Id = 3, Nombre = "Uruguay"   }
            );

            builder.Entity<Sexo>().HasData(
                new Sexo { Id = 1, Descripcion = "Hombre" },
                new Sexo { Id = 2, Descripcion = "Mujer"  }
            );

            builder.Entity<Persona>().HasData(
                new Persona {Id=1, Nombre="Marcos",Apellido="Pintos",TipoDocumento="dni", NroDocumento="12345678", 
                                FechaNacimiento = DateTime.Now.Date, PaisId=1, SexoId=1,
                                Email="micorreo@gmail.com", Telefono="1111"},
                new Persona {Id=2, Nombre="Maria",Apellido="Lopez",TipoDocumento="dni", NroDocumento="12345679", 
                                FechaNacimiento = DateTime.Now.Date, PaisId=2, SexoId=2,
                                Email="lucas@gmail.com", Telefono="2222"}
            );
        }
    }
}
