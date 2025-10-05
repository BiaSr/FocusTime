using FocusTime.Domain.Entities;
using FocusTime.Application.UseCases;
using FocusTime.Infrastructure.Services;
using FocusTime.Infrastructure.Data;
using FocusTime.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FocusTime.Presentation {
    class Program {
        private static DisciplinaRepository disciplinaRepo = null!;
        private static AtividadeRepository atividadeRepo = null!;
        private static readonly ConsoleNotificationService notificacao = new();

        static async Task Main(string[] args) {
            // Configurar as opções do DbContext
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=FocusTime.db")
                .Options;

            // Criar o contexto com as opções
            using var context = new AppDbContext(options);
            context.Database.EnsureCreated();

            disciplinaRepo = new DisciplinaRepository(context);
            atividadeRepo = new AtividadeRepository(context);

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
                    case "1": await CadastrarDisciplina(); break;
                    case "2": await CadastrarAtividade(); break;
                    case "3": await ListarDisciplinas(); break;
                    case "4": await ListarAtividades(); break;
                    case "5": await GerarPlano(); break;
                    case "6": await ExcluirDisciplina(); break;
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

        // ----------------------
        // CADASTRAR DISCIPLINA
        // ----------------------
        private static async Task CadastrarDisciplina() {
            Console.WriteLine("Cadastre sua Disciplina");

            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "Sem Nome";

            int carga = LerInteiro("Carga horária (em horas): ", min: 1);

            Console.Write("Digite o tipo da Disciplina (1 = Teórica, 2 = Prática): ");
            int tipo = LerInteiro("", min: 1, max: 2);

            Disciplina disciplina = tipo == 1
                ? new DisciplinaTeorica(nome, carga)
                : new DisciplinaPratica(nome, carga);

            await disciplinaRepo.AdicionarAsync(disciplina);
            Console.WriteLine($"✅ Disciplina \"{nome}\" cadastrada com sucesso!");
        }

        // ----------------------
        // CADASTRAR ATIVIDADE
        // ----------------------
        private static async Task CadastrarAtividade() {
            var disciplinas = await disciplinaRepo.ListarAsync();

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

            var disciplinaEscolhida = disciplinas[escolhaDisciplina];
            Atividade atividade = tipo switch {
                1 => new Prova(descricao, data, disciplinaEscolhida),
                2 => new Trabalho(descricao, data, disciplinaEscolhida),
                3 => new Revisao(descricao, data, disciplinaEscolhida),
                _ => new Prova(descricao, data, disciplinaEscolhida)
            };

            await atividadeRepo.AdicionarAsync(atividade);
            Console.WriteLine($"✅ Atividade \"{descricao}\" cadastrada com sucesso!");
        }

        // ----------------------
        // LISTAR DISCIPLINAS
        // ----------------------
        private static async Task ListarDisciplinas() {
            var disciplinas = await disciplinaRepo.ListarAsync();

            Console.WriteLine("Disciplinas cadastradas:");
            if (disciplinas.Count == 0) {
                Console.WriteLine("Nenhuma disciplina cadastrada.");
                return;
            }

            foreach (var d in disciplinas) {
                Console.WriteLine($"{d.Nome} - {d.GetTipo()} - {d.CargaHoraria}h");
            }
        }

        // ----------------------
        // LISTAR ATIVIDADES
        // ----------------------
        private static async Task ListarAtividades() {
            var atividades = await atividadeRepo.ListarAsync();

            Console.WriteLine("Atividades cadastradas:");
            if (atividades.Count == 0) {
                Console.WriteLine("Nenhuma atividade cadastrada.");
                return;
            }

            foreach (var a in atividades) {
                Console.WriteLine($"[{a.GetType().Name}] {a.Descricao} - {a.Disciplina.Nome} - Entrega: {a.DataEntrega:dd/MM/yyyy}");
            }
        }

        // ----------------------
        // EXCLUIR DISCIPLINA
        // ----------------------
        private static async Task ExcluirDisciplina() {
            var disciplinas = await disciplinaRepo.ListarAsync();

            if (disciplinas.Count == 0) {
                Console.WriteLine("Nenhuma disciplina cadastrada para excluir.");
                return;
            }

            Console.WriteLine("Escolha a disciplina para excluir:");
            for (int i = 0; i < disciplinas.Count; i++) {
                Console.WriteLine($"{i + 1}. {disciplinas[i].Nome}");
            }

            int escolha = LerInteiro("Número da disciplina: ", min: 1, max: disciplinas.Count) - 1;
            var disciplinaRemovida = disciplinas[escolha];

            await disciplinaRepo.ExcluirAsync(disciplinaRemovida.Id);
            await atividadeRepo.RemoverPorDisciplinaAsync(disciplinaRemovida.Id);

            Console.WriteLine($"✅ Disciplina \"{disciplinaRemovida.Nome}\" e suas atividades associadas foram removidas com sucesso!");
        }

        // ----------------------
        // GERAR ALERTA
        // ----------------------
        private static async Task GerarPlano() {
            var disciplinas = await disciplinaRepo.ListarAsync();
            var atividades = await atividadeRepo.ListarAsync();

            if (atividades.Count == 0) {
                Console.WriteLine("Nenhuma atividade cadastrada.");
                return;
            }

            var cronograma = new GerarAlerta();
            var plano = cronograma.CriarPlano(disciplinas, atividades);

            Console.WriteLine("\n📅 ALERTA DE ESTUDOS:");
            Console.WriteLine(new string('-', 40));

            foreach (var tarefa in plano) {
                notificacao.Notificar(tarefa);
            }

            Console.WriteLine("\n✅ Fim do Cronograma!");
        }

        // ----------------------
        // AUXILIARES
        // ----------------------
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