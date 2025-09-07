namespace FocusTime.Domain.Entities {
    public abstract class Disciplina {
        public string Nome { get; private set; }
        public int CargaHoraria { get; private set; }

        protected Disciplina(string nome, int cargaHoraria) {
            Nome = nome;
            CargaHoraria = cargaHoraria;
        }

        public abstract string GetTipo();
    }
}
