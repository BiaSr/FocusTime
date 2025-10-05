namespace FocusTime.Domain.Entities {
    public class Prova : Atividade {
        protected Prova() : base() { }

        public Prova(string descricao, DateTime dataEntrega, Disciplina disciplina)
            : base(descricao, dataEntrega, disciplina) { }

        public override string GetTipo() => "Prova";
    }
}
