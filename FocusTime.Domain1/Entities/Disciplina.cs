namespace FocusTime.Domain.Entities {
    public abstract class Disciplina {
        public int Id { get; set; } // EF precisa set
        public string Nome { get; set; } = "";
        public int CargaHoraria { get; set; }

        protected Disciplina() { }

        protected Disciplina(string nome, int cargaHoraria) {
            Nome = nome;
            CargaHoraria = cargaHoraria;
        }

        public abstract string Tipo { get; }
    }
}
