namespace FocusTime.Domain.Entities {
    public class DisciplinaTeorica : Disciplina {
        public DisciplinaTeorica(string nome, int cargaHoraria)
            : base(nome, cargaHoraria) { }

        public override string GetTipo() => "Teórica";
    }
}
