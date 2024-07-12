namespace NDAccountManager.Core.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
