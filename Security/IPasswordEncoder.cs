namespace JournalApiApp.Security
{
    public interface IPasswordEncoder
    {
        string Encode(string password);
    }
}
