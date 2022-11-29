using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PruebaDALE.Models
{
    public partial class ModeloPrueba : DbContext
    {
        public ModeloPrueba()
            : base("name=ModeloPrueba")
        {
        }

        public virtual DbSet<cliente> cliente { get; set; }
        public virtual DbSet<producto> producto { get; set; }
        public virtual DbSet<Comprobante> Comprobantes { get; set; }
        public virtual DbSet<ComprobanteDetalle> ComprobanteDetalles { get; set; }
        public virtual DbSet<Medio_Pago> Medio_Pagos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cliente>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.Nombre)
                .IsUnicode(false);
        }
    }
}
