using UserManagement.Common.EfCore;

namespace UserManagement.Domain
{
    public class User : Entity<int>
    {
        public User()
        {
            
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
