using FocusTime.Presentation.Wpf.ViewModels;
using System.Windows;

namespace FocusTime.Presentation.Wpf {
    public partial class MainWindow : Window {
        private readonly MainViewModel _vm;

        public MainWindow() {
            InitializeComponent();
            _vm = new MainViewModel();
            DataContext = _vm;
        }

        private void AdicionarDisciplina_Click(object sender, RoutedEventArgs e) {
            _vm.AdicionarDisciplina();
        }

        private void AdicionarAtividade_Click(object sender, RoutedEventArgs e) {
            _vm.AdicionarAtividade();
        }
    }
}