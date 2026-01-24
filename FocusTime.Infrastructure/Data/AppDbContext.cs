using FocusTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FocusTime.Infrastructure.Data {
    public class AppDbContext : DbContext {
        public DbSet<Disciplina> Disciplinas => Set<Disciplina>();
        public DbSet<Atividade> Atividades => Set<Atividade>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=FocusTime.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Herança no mesmo tabelão (TPH)
            modelBuilder.Entity<Disciplina>()
                .HasDiscriminator<string>("TipoDisciplina")
                .HasValue<DisciplinaTeorica>("Teorica")
                .HasValue<DisciplinaPratica>("Pratica");

            modelBuilder.Entity<Atividade>()
                .HasDiscriminator<string>("TipoAtividade")
                .HasValue<Prova>("Prova")
                .HasValue<Trabalho>("Trabalho")
                .HasValue<Revisao>("Revisao");

            modelBuilder.Entity<Atividade>()
                .HasOne(a => a.Disciplina)
                .WithMany()
                .HasForeignKey(a => a.DisciplinaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
