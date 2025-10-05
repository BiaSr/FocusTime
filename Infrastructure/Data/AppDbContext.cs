using FocusTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FocusTime.Infrastructure.Data {
    public class AppDbContext : DbContext {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Atividade> Atividades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlite("Data Source=FocusTime.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Disciplina>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<DisciplinaTeorica>().HasBaseType<Disciplina>();
            modelBuilder.Entity<DisciplinaPratica>().HasBaseType<Disciplina>();

            modelBuilder.Entity<Atividade>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Prova>().HasBaseType<Atividade>();
            modelBuilder.Entity<Trabalho>().HasBaseType<Atividade>();
            modelBuilder.Entity<Revisao>().HasBaseType<Atividade>();

            modelBuilder.Entity<Atividade>()
                .HasOne(a => a.Disciplina)
                .WithMany()
                .HasForeignKey(a => a.DisciplinaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}