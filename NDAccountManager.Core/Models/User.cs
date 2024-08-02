namespace NDAccountManager.Core.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<SharedAccount> SharedAccounts { get; set; }
    }
}
