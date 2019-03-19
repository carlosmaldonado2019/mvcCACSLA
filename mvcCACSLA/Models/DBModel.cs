namespace mvcCACSLA.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<Carreras> Carreras { get; set; }
        public virtual DbSet<Indicadores> Indicadores { get; set; }
        public virtual DbSet<Indicadores_archivos> Indicadores_archivos { get; set; }
        public virtual DbSet<Indicadores_detalle> Indicadores_detalle { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Usuarios_niveles> Usuarios_niveles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carreras>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Carreras>()
                .Property(e => e.Ini)
                .IsUnicode(false);

            modelBuilder.Entity<Carreras>()
                .HasMany(e => e.Indicadores_archivos)
                .WithRequired(e => e.Carreras)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Carreras>()
                .HasMany(e => e.Indicadores_detalle)
                .WithRequired(e => e.Carreras)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Carreras>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.Carreras)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Indicadores>()
                .Property(e => e.DescripcionEstandar)
                .IsUnicode(false);

            modelBuilder.Entity<Indicadores>()
                .Property(e => e.DescripcionVariable)
                .IsUnicode(false);

            modelBuilder.Entity<Indicadores>()
                .Property(e => e.DescripcionReferente)
                .IsUnicode(false);

            modelBuilder.Entity<Indicadores>()
                .Property(e => e.Anexo)
                .IsUnicode(false);

            modelBuilder.Entity<Indicadores>()
                .HasMany(e => e.Indicadores_archivos)
                .WithRequired(e => e.Indicadores)
                .HasForeignKey(e => new { e.IdEstandar, e.IdVariable });

            modelBuilder.Entity<Indicadores>()
                .HasMany(e => e.Indicadores_detalle)
                .WithRequired(e => e.Indicadores)
                .HasForeignKey(e => new { e.IdEstandar, e.IdVariable })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Indicadores_archivos>()
                .Property(e => e.RutaArchivo)
                .IsUnicode(false);

            modelBuilder.Entity<Indicadores_archivos>()
                .Property(e => e.NombreArchivo)
                .IsUnicode(false);

            modelBuilder.Entity<Indicadores_detalle>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Pass)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Indicadores_archivos)
                .WithRequired(e => e.Usuarios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Indicadores_detalle)
                .WithRequired(e => e.Usuarios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios_niveles>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios_niveles>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.Usuarios_niveles)
                .WillCascadeOnDelete(false);
        }
    }
}
