namespace JournalApiApp.Controllers.ApiMessages
{
    // класс базовых сообщений сервера
    public class BaseApiMessages
    {
        // простое текстовое сообщение
        public record StringMessage(string Message);
    }
}
