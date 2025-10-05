namespace FocusTime.Domain.Entities {
    public class Trabalho : Atividade {
        // Construtor sem parâmetros para EF Core
        protected Trabalho() : base() { }

        public Trabalho(string descricao, DateTime dataEntrega, Disciplina disciplina)
            : base(descricao, dataEntrega, disciplina) { }

        public override string GetTipo() => "Trabalho";
    }
}
