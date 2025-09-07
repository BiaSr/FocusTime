namespace FocusTime.Domain.Entities {
    public class Trabalho : Atividade {
        public Trabalho(string descricao, DateTime dataEntrega, Disciplina disciplina)
            : base(descricao, dataEntrega, disciplina) { }

        public override string GetTipo() => "Trabalho";
    }
}
