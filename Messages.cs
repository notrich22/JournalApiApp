namespace JournalApiApp
{
    public class Messages
    {
        public record UserData(string Login, string Password, string Group);
        public record UserLogin(string Login, string Password);
        public record StringMessage(string text);
    }
}
