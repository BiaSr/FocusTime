namespace FocusTime.Domain.Entities {
    public class Trabalho : Atividade {
        protected Trabalho() : base() { }

        public Trabalho(string descricao, DateTime dataEntrega, Disciplina disciplina)
            : base(descricao, dataEntrega, disciplina) { }

        public override string GetTipo() => "Trabalho";
    }
}
