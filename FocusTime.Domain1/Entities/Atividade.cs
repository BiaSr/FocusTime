using System;

namespace FocusTime.Domain.Entities {
    public abstract class Atividade {
        public int Id { get; set; }
        public string Descricao { get; set; } = "";
        public DateTime DataEntrega { get; set; }

        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; } = null!;

        protected Atividade() { }

        protected Atividade(string descricao, DateTime dataEntrega, int disciplinaId) {
            Descricao = descricao;
            DataEntrega = dataEntrega;
            DisciplinaId = disciplinaId;
        }

        public abstract string Tipo { get; }
    }
}