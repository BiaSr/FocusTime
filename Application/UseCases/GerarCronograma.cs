using FocusTime.Domain.Entities;

namespace FocusTime.Application.UseCases {
    public class GerarAlerta {
        public List<string> CriarPlano(List<Disciplina> disciplinas, List<Atividade> atividades) {
            var plano = new List<string>();

            foreach (var atividade in atividades.OrderBy(a => a.DataEntrega)) {
                plano.Add(
                    $"[Até {atividade.DataEntrega:dd/MM/yyyy}] " +
                    $"{atividade.GetTipo()} - {atividade.Descricao} " +
                    $"({atividade.Disciplina.Nome} - {atividade.Disciplina.GetTipo()})"
                );
            }

            return plano;
        }
    }
}
