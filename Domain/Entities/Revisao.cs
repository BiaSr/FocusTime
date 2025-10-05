namespace FocusTime.Domain.Entities {
    public class Revisao : Atividade {
        // Construtor sem parâmetros para EF Core
        protected Revisao() : base() { }

        public Revisao(string descricao, DateTime dataEntrega, Disciplina disciplina)
            : base(descricao, dataEntrega, disciplina) { }

        public override string GetTipo() => "Revisão";
    }
}
