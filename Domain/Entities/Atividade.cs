namespace FocusTime.Domain.Entities {
    public abstract class Atividade {
        public string Descricao { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public Disciplina Disciplina { get; private set; }

        protected Atividade(string descricao, DateTime dataEntrega, Disciplina disciplina) {
            Descricao = descricao;
            DataEntrega = dataEntrega;
            Disciplina = disciplina;
        }

        public abstract string GetTipo();
    }
}
