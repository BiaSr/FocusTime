using FocusTime.Application.Interfaces;
using FocusTime.Domain.Entities;
using FocusTime.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FocusTime.Infrastructure.Repositories {
    public class AtividadeRepository : IAtividadeRepository {
        private readonly AppDbContext _context;

        public AtividadeRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<List<Atividade>> ListarAsync()
            => await _context.Atividades.Include(a => a.Disciplina)
                                        .AsNoTracking()
                                        .ToListAsync();

        public async Task AdicionarAsync(Atividade atividade) {
            _context.Atividades.Add(atividade);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id) {
            var a = await _context.Atividades.FirstOrDefaultAsync(x => x.Id == id);
            if (a == null) return;

            _context.Atividades.Remove(a);
            await _context.SaveChangesAsync();
        }
    }
}
