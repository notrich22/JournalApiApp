namespace JournalApiApp
{
    public class Records
    {
        public record UserData(string Login, string Password, string Group);
        public record StudentData(string FullName, int GroupId, int UserId);
        public record UserLogin(string Login, string Password);
        public record StringMessage(string text);
        public record IdData(int id);
        public record DoubleIntData(int id1, int id2);
        public record TripleIntData(int id1, int id2, int id3);
        public record LoginData(string login);
    }
}
