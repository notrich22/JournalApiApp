using Microsoft.VisualBasic;

namespace JournalApiApp.Model.Entities.Access
{
    public class UsersGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
