namespace FocusTime.Domain.Entities {
    public abstract class Disciplina {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }

        protected Disciplina(string nome, int cargaHoraria) {
            Id = Guid.NewGuid();
            Nome = nome;
            CargaHoraria = cargaHoraria;
        }

        public abstract string GetTipo();
    }
}