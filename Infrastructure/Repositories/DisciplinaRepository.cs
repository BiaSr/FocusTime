using FocusTime.Domain.Entities;
using FocusTime.Application.Interfaces;
using FocusTime.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FocusTime.Infrastructure.Repositories {
    public class DisciplinaRepository : IDisciplinaRepository {
        private readonly AppDbContext _context;

        public DisciplinaRepository(AppDbContext context) {
            _context = context;
        }

        public async Task AdicionarAsync(Disciplina disciplina) {
            _context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Disciplina>> ListarAsync() {
            return await _context.Disciplinas.ToListAsync();
        }

        public async Task ExcluirAsync(Guid id) {
            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina != null) {
                _context.Disciplinas.Remove(disciplina);
                await _context.SaveChangesAsync();
            }
        }
    }
}
