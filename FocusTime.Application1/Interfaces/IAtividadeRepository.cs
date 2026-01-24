using FocusTime.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocusTime.Application.Interfaces {
    public interface IAtividadeRepository {
        Task<List<Atividade>> ListarAsync();
        Task AdicionarAsync(Atividade atividade);
        Task RemoverAsync(int id);
    }
}

