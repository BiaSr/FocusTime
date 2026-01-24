using System;

namespace FocusTime.Domain.Entities {
    public class Revisao : Atividade {
        public Revisao() { }
        public Revisao(string descricao, DateTime dataEntrega, int disciplinaId)
            : base(descricao, dataEntrega, disciplinaId) { }

        public override string Tipo => "Revisão";
    }
}
