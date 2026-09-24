using Microsoft.EntityFrameworkCore;
using adm_colegio.Models;

namespace adm_colegio.Data
{
    public class db_context : DbContext
    {
        public db_context(DbContextOptions<db_context> options) : base(options) { }

        public DbSet<Persona> personas => Set<Persona>();
        public DbSet<Materia> materias => Set<Materia>();
        public DbSet<Asigna_materia> asignacion_materias_profesor => Set<Asigna_materia>();
        public DbSet<Matricula_calificacion> matricula_Calificacions => Set<Matricula_calificacion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo EXACTO a las tablas de tu SQL Server
            modelBuilder.Entity<Persona>().ToTable("Personas");
            modelBuilder.Entity<Materia>().ToTable("Materias");
            modelBuilder.Entity<Asigna_materia>().ToTable("AsignacionMateriasProfesor");
            modelBuilder.Entity<Matricula_calificacion>().ToTable("MatriculasYCalificaciones");

            // Primary Keys y Configuración de Autoincremento (Identity)
            modelBuilder.Entity<Persona>().HasKey(p => p.i_id);
            modelBuilder.Entity<Persona>().Property(p => p.i_id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Materia>().HasKey(m => m.i_id);
            modelBuilder.Entity<Materia>().Property(m => m.i_id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Asigna_materia>().HasKey(a => a.i_id);
            modelBuilder.Entity<Asigna_materia>().Property(a => a.i_id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Matricula_calificacion>().HasKey(mc => mc.i_id);
            modelBuilder.Entity<Matricula_calificacion>().Property(mc => mc.i_id).ValueGeneratedOnAdd();

            // Foreign Keys
            modelBuilder.Entity<Asigna_materia>()
                .HasOne(a => a.profesor)
                .WithMany()
                .HasForeignKey(a => a.i_profesor_id);

            modelBuilder.Entity<Asigna_materia>()
                .HasOne(a => a.Materia)
                .WithMany()
                .HasForeignKey(a => a.i_materia_id);

            modelBuilder.Entity<Matricula_calificacion>()
                .HasOne(m => m.Alumno)
                .WithMany()
                .HasForeignKey(m => m.i_alumno_id);

            modelBuilder.Entity<Matricula_calificacion>()
                .HasOne(m => m.Materia)
                .WithMany()
                .HasForeignKey(m => m.i_materia_id);
        }
    }
}