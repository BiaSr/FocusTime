namespace FocusTime.Infrastructure.Services {
    public class ConsoleNotificationService {
        public void Notificar(string mensagem) {
            Console.WriteLine($"[NOTIFICAÇÃO] {mensagem}");
        }
    }
}
