using FocusTime.Domain.Entities;

namespace FocusTime.Application.Interfaces {
    public interface IDisciplinaRepository {
        Task AdicionarAsync(Disciplina disciplina);
        Task<List<Disciplina>> ListarAsync();
        Task ExcluirAsync(Guid id);
    }
}