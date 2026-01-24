using System;

namespace FocusTime.Domain.Entities {
    public class Prova : Atividade {
        public Prova() { }
        public Prova(string descricao, DateTime dataEntrega, int disciplinaId)
            : base(descricao, dataEntrega, disciplinaId) { }

        public override string Tipo => "Prova";
    }
}