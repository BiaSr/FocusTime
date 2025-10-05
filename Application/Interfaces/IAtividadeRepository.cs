using FocusTime.Domain.Entities;

namespace FocusTime.Application.Interfaces {
    public interface IAtividadeRepository {
        Task AdicionarAsync(Atividade atividade);
        Task<List<Atividade>> ListarAsync();
        Task ExcluirAsync(Guid id);
        Task RemoverPorDisciplinaAsync(Guid disciplinaId);
    }
}
