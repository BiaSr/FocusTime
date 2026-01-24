using FocusTime.Application.Interfaces;
using FocusTime.Domain.Entities;
using FocusTime.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FocusTime.Infrastructure.Repositories {
    public class DisciplinaRepository : IDisciplinaRepository {
        private readonly AppDbContext _context;

        public DisciplinaRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<List<Disciplina>> ListarAsync()
            => await _context.Disciplinas.AsNoTracking().ToListAsync();

        public async Task AdicionarAsync(Disciplina disciplina) {
            _context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id) {
            var d = await _context.Disciplinas.FirstOrDefaultAsync(x => x.Id == id);
            if (d == null) return;

            _context.Disciplinas.Remove(d);
            await _context.SaveChangesAsync();
        }
    }
}
