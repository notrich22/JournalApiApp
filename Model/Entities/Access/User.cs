namespace JournalApiApp.Model.Entities.Access
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UsersGroup UserGroup { get; set; }
        public int UserGroupId { get; set; }
    }
}
