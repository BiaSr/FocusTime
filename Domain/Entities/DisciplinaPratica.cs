namespace FocusTime.Domain.Entities {
    public class DisciplinaPratica : Disciplina {
        public DisciplinaPratica(string nome, int cargaHoraria)
            : base(nome, cargaHoraria) { }

        public override string GetTipo() => "Prática";
    }
}