using System;

namespace FocusTime.Domain.Entities {
    public class Trabalho : Atividade {
        public Trabalho() { }
        public Trabalho(string descricao, DateTime dataEntrega, int disciplinaId)
            : base(descricao, dataEntrega, disciplinaId) { }

        public override string Tipo => "Trabalho";
    }
}

