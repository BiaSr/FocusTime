namespace FocusTime.Domain.Entities {
    public abstract class Atividade {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime DataEntrega { get; set; }
        public Disciplina Disciplina { get; set; } = null!;
        public Guid DisciplinaId { get; set; }

        protected Atividade() { }

        protected Atividade(string descricao, DateTime dataEntrega, Disciplina disciplina) {
            Id = Guid.NewGuid();
            Descricao = descricao;
            DataEntrega = dataEntrega;
            Disciplina = disciplina;
            DisciplinaId = disciplina.Id;
        }

        public abstract string GetTipo();
    }
}