using ApisConvenciones9.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApisConvenciones9.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext (DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Categoria_Actividades> Cat_Actividades { get; set; }
        public DbSet<Categoria_Recomendacion> Cat_Recomendaciones { get; set; }
        public DbSet<Categoria_Usuario> Cat_Usuarios { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Hotel> Hoteles { get; set; }
        public DbSet<Recomendacion> Recomendaciones { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<Convencionista> Convencionistas { get; set; }
        public DbSet<Vuelo> Vuelos { get; set; }
        public DbSet<Version_App> Versiones_App { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
