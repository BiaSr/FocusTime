using FocusTime.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocusTime.Application.Interfaces {
    public interface IDisciplinaRepository {
        Task<List<Disciplina>> ListarAsync();
        Task AdicionarAsync(Disciplina disciplina);
        Task RemoverAsync(int id);
    }
}
