namespace NDAccountManager.Core.Models
{
    public class Account : BaseEntity
    {
        public string Platform { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<SharedAccount> SharedAccounts { get; set; }
    }
}
