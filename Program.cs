using FocusTime.Domain.Entities;
using FocusTime.Application.UseCases;
using FocusTime.Infrastructure.Services;

namespace FocusTime.Presentation {
    class Program {
        private static List<Disciplina> disciplinas = new();
        private static List<Atividade> atividades = new();
        private static readonly ConsoleNotificationService notificacao = new();

        static void Main(string[] args) {
            Console.WriteLine("Bem-vindo ao FocusTime - Sua Agenda Inteligente!\n");

            bool executando = true;
            while (executando) {
                Console.WriteLine("\n=== MENU PRINCIPAL ===");
                Console.WriteLine("1 - Cadastrar Disciplina");
                Console.WriteLine("2 - Cadastrar Atividade");
                Console.WriteLine("3 - Listar Disciplinas");
                Console.WriteLine("4 - Listar Atividades");
                Console.WriteLine("5 - Gerar Alerta");
                Console.WriteLine("6 - Excluir Disciplina");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                string? opcao = Console.ReadLine();
                Console.WriteLine();

                switch (opcao) {
                    case "1":
                        CadastrarDisciplina();
                        break;
                    case "2":
                        CadastrarAtividade();
                        break;
                    case "3":
                        ListarDisciplinas();
                        break;
                    case "4":
                        ListarAtividades();
                        break;
                    case "5":
                        GerarPlano();
                        break;
                    case "6":
                        ExcluirDisciplina();
                        break;
                    case "0":
                        executando = false;
                        Console.WriteLine("Saindo do FocusTime. Até logo!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        // Cadastro de Disciplina

        private static void CadastrarDisciplina() {
            Console.WriteLine("Cadastre sua Disciplina");

            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "Sem Nome";

            int carga = LerInteiro("Carga horária (em horas): ", min: 1);

            Console.Write("Digite o tipo da Disciplina (1 = Teórica, 2 = Prática): ");
            int tipo = LerInteiro("", min: 1, max: 2);

            Disciplina disciplina = tipo == 1
                ? new DisciplinaTeorica(nome, carga)
                : new DisciplinaPratica(nome, carga);

            disciplinas.Add(disciplina);
            Console.WriteLine($"Disciplina \"{nome}\" cadastrada com sucesso!");
        }
        // Cadastro de Atividade

        private static void CadastrarAtividade() {
            if (disciplinas.Count == 0) {
                Console.WriteLine("Nenhuma disciplina cadastrada. Cadastre uma disciplina antes.");
                return;
            }

            Console.WriteLine("Cadastre sua Atividade");

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine() ?? "Sem Descrição";

            DateTime data = LerData("Data de entrega (dd/mm/aaaa): ");

            Console.WriteLine("Selecione a disciplina:");
            for (int i = 0; i < disciplinas.Count; i++) {
                Console.WriteLine($"{i + 1} - {disciplinas[i].Nome} ({disciplinas[i].GetTipo()})");
            }
            int escolhaDisciplina = LerInteiro("Opção: ", min: 1, max: disciplinas.Count) - 1;

            Console.Write("Digite o tipo da atividade (1 = Prova, 2 = Trabalho, 3 = Revisão): ");
            int tipo = LerInteiro("", min: 1, max: 3);

            Atividade atividade = tipo switch {
                1 => new Prova(descricao, data, disciplinas[escolhaDisciplina]),
                2 => new Trabalho(descricao, data, disciplinas[escolhaDisciplina]),
                3 => new Revisao(descricao, data, disciplinas[escolhaDisciplina]),
                _ => new Prova(descricao, data, disciplinas[escolhaDisciplina])
            };

            atividades.Add(atividade);
            Console.WriteLine($"Atividade \"{descricao}\" cadastrada com sucesso!");
        }

        // Listar Disciplinas

        private static void ListarDisciplinas() {
            Console.WriteLine("Disciplinas cadastradas:");

            if (disciplinas.Count == 0) {
                Console.WriteLine("Nenhuma disciplina cadastrada.");
                return;
            }

            for (int i = 0; i < disciplinas.Count; i++) {
                Console.WriteLine($"{i + 1}. {disciplinas[i].Nome} - {disciplinas[i].GetTipo()} - {disciplinas[i].CargaHoraria}h");
            }
        }

        // Listar Atividades

        private static void ListarAtividades() {
            Console.WriteLine("Atividades cadastradas:");

            if (atividades.Count == 0) {
                Console.WriteLine("Nenhuma atividade cadastrada.");
                return;
            }

            for (int i = 0; i < atividades.Count; i++) {
                Console.WriteLine($"{i + 1}. [{atividades[i].GetType().Name}] {atividades[i].Descricao} - " +
                                  $"{atividades[i].Disciplina.Nome} ({atividades[i].Disciplina.GetTipo()}) - " +
                                  $"Entrega: {atividades[i].DataEntrega:dd/MM/yyyy}");
            }
        }

        // Excluir Disciplina

        private static void ExcluirDisciplina() {
            if (disciplinas.Count == 0) {
                Console.WriteLine("Nenhuma disciplina cadastrada para excluir.");
                return;
            }

            Console.WriteLine("Escolha a disciplina para excluir:");
            for (int i = 0; i < disciplinas.Count; i++) {
                Console.WriteLine($"{i + 1}. {disciplinas[i].Nome} - {disciplinas[i].GetTipo()}");
            }

            int escolha = LerInteiro("Número da disciplina: ", min: 1, max: disciplinas.Count) - 1;

            var disciplinaRemovida = disciplinas[escolha];

            // Remover atividades ligadas a esta disciplina
            atividades.RemoveAll(a => a.Disciplina == disciplinaRemovida);

            disciplinas.RemoveAt(escolha);

            Console.WriteLine($"Disciplina \"{disciplinaRemovida.Nome}\" e suas atividades associadas foram removidas com sucesso!");
        }

        private static void GerarPlano() {
            if (atividades.Count == 0) {
                Console.WriteLine("Nenhuma atividade cadastrada.");
                return;
            }

            var cronograma = new GerarAlerta();
            var plano = cronograma.CriarPlano(disciplinas, atividades);

            Console.WriteLine("\n ALERTA DE ESTUDOS:");
            Console.WriteLine(new string('-', 40));

            foreach (var tarefa in plano) {
                notificacao.Notificar(tarefa);
            }

            Console.WriteLine("\n Fim do Cronograma!");
        }

        private static int LerInteiro(string mensagem, int min = int.MinValue, int max = int.MaxValue) {
            int valor;
            while (true) {
                if (!string.IsNullOrWhiteSpace(mensagem))
                    Console.Write(mensagem);

                string? entrada = Console.ReadLine();

                if (int.TryParse(entrada, out valor) && valor >= min && valor <= max) {
                    return valor;
                }

                Console.WriteLine($"Entrada inválida. Digite um número entre {min} e {max}.");
            }
        }

        private static DateTime LerData(string mensagem) {
            DateTime data;
            while (true) {
                Console.Write(mensagem);
                string? entrada = Console.ReadLine();

                if (DateTime.TryParse(entrada, out data) && data >= DateTime.Today) {
                    return data;
                }

                Console.WriteLine("Data inválida. Use o formato dd/mm/aaaa e escolha uma data futura.");
            }
        }
    }
}
