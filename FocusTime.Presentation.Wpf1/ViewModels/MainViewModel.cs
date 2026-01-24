using FocusTime.Domain.Entities;
using FocusTime.Infrastructure.Data;
using FocusTime.Infrastructure.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FocusTime.Presentation.Wpf.ViewModels {
    public class MainViewModel : INotifyPropertyChanged {
        private readonly AppDbContext _context;
        private readonly DisciplinaRepository _disciplinaRepo;
        private readonly AtividadeRepository _atividadeRepo;

        public ObservableCollection<Disciplina> Disciplinas { get; } = new();
        public ObservableCollection<Atividade> Atividades { get; } = new();

        // ---- CAMPOS DA DISCIPLINA ----
        private string _nomeDisciplina = "";
        public string NomeDisciplina {
            get => _nomeDisciplina;
            set { _nomeDisciplina = value; OnPropertyChanged(); }
        }

        private int _cargaHoraria = 1;
        public int CargaHoraria {
            get => _cargaHoraria;
            set { _cargaHoraria = value; OnPropertyChanged(); }
        }

        private string _tipoDisciplina = "Teórica";
        public string TipoDisciplina {
            get => _tipoDisciplina;
            set { _tipoDisciplina = value; OnPropertyChanged(); }
        }

        public Disciplina? DisciplinaSelecionada { get; set; }

        // ---- CAMPOS DA ATIVIDADE ----
        private string _descricaoAtividade = "";
        public string DescricaoAtividade {
            get => _descricaoAtividade;
            set { _descricaoAtividade = value; OnPropertyChanged(); }
        }

        private DateTime _dataEntrega = DateTime.Today.AddDays(1);
        public DateTime DataEntrega {
            get => _dataEntrega;
            set { _dataEntrega = value; OnPropertyChanged(); }
        }

        private string _tipoAtividade = "Prova";
        public string TipoAtividade {
            get => _tipoAtividade;
            set { _tipoAtividade = value; OnPropertyChanged(); }
        }

        public MainViewModel() {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();

            _disciplinaRepo = new DisciplinaRepository(_context);
            _atividadeRepo = new AtividadeRepository(_context);

            CarregarDados();
        }

        private async void CarregarDados() {
            Disciplinas.Clear();
            foreach (var d in await _disciplinaRepo.ListarAsync())
                Disciplinas.Add(d);

            Atividades.Clear();
            foreach (var a in await _atividadeRepo.ListarAsync())
                Atividades.Add(a);
        }

        public async void AdicionarDisciplina() {
            if (string.IsNullOrWhiteSpace(NomeDisciplina)) return;

            Disciplina d = TipoDisciplina == "Prática"
                ? new DisciplinaPratica(NomeDisciplina, CargaHoraria)
                : new DisciplinaTeorica(NomeDisciplina, CargaHoraria);

            await _disciplinaRepo.AdicionarAsync(d);

            NomeDisciplina = "";
            CargaHoraria = 1;
            TipoDisciplina = "Teórica";

            CarregarDados();
        }

        public async void AdicionarAtividade() {
            if (DisciplinaSelecionada == null) return;
            if (string.IsNullOrWhiteSpace(DescricaoAtividade)) return;

            Atividade a = TipoAtividade switch {
                "Trabalho" => new Trabalho(DescricaoAtividade, DataEntrega, DisciplinaSelecionada.Id),
                "Revisão" => new Revisao(DescricaoAtividade, DataEntrega, DisciplinaSelecionada.Id),
                _ => new Prova(DescricaoAtividade, DataEntrega, DisciplinaSelecionada.Id)
            };

            await _atividadeRepo.AdicionarAsync(a);

            DescricaoAtividade = "";
            DataEntrega = DateTime.Today.AddDays(1);
            TipoAtividade = "Prova";

            CarregarDados();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
