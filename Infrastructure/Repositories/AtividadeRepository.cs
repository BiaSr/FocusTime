using FocusTime.Domain.Entities;
using FocusTime.Application.Interfaces;
using FocusTime.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FocusTime.Infrastructure.Repositories {
    public class AtividadeRepository : IAtividadeRepository {
        private readonly AppDbContext _context;

        public AtividadeRepository(AppDbContext context) {
            _context = context;
        }

        public async Task AdicionarAsync(Atividade atividade) {
            _context.Atividades.Add(atividade);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Atividade>> ListarAsync() {
            return await _context.Atividades.Include(a => a.Disciplina).ToListAsync();
        }

        public async Task ExcluirAsync(Guid id) {
            var atividade = await _context.Atividades.FindAsync(id);
            if (atividade != null) {
                _context.Atividades.Remove(atividade);
                await _context.SaveChangesAsync();
            }
        }

        // Método que faltava implementar
        public async Task RemoverPorDisciplinaAsync(Guid disciplinaId) {
            var atividades = await _context.Atividades
                .Where(a => a.DisciplinaId == disciplinaId)
                .ToListAsync();

            if (atividades.Any()) {
                _context.Atividades.RemoveRange(atividades);
                await _context.SaveChangesAsync();
            }
        }
    }
}