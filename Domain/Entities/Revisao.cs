namespace FocusTime.Domain.Entities {
    public class Revisao : Atividade {
        public Revisao(string descricao, DateTime dataEntrega, Disciplina disciplina)
            : base(descricao, dataEntrega, disciplina) { }

        public override string GetTipo() => "Revisão";
    }
}
