using FocusTime.Domain.Entities;

namespace FocusTime.Application.UseCases {
    public class GerarPlanoDeEstudosUseCase {
        public List<string> GerarPlano(List<Disciplina> disciplinas, List<Atividade> atividades) {
            List<string> planoDeEstudos = new List<string>();

            atividades.Sort((a, b) => a.DataEntrega.CompareTo(b.DataEntrega));

            foreach (Atividade atividade in atividades) {
                string linhaDoPlano =
                    "Até " + atividade.DataEntrega.ToString("dd/MM/yyyy") +
                    ": " + atividade.Tipo +
                    " - " + atividade.Descricao +
                    " (" + atividade.Disciplina.Nome +
                    " | " + atividade.Disciplina.Tipo + ")";

                planoDeEstudos.Add(linhaDoPlano);
            }

            return planoDeEstudos;
        }
    }
}