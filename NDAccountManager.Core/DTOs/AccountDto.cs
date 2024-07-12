namespace NDAccountManager.Core.DTOs
{
    public class AccountDto:BaseDto
    {
        public string Platform { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }//buna da gerek olmayabilir idk
        public string IPAddress { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        // daha fazla ozellestirmeler iceren dto lar olustur
        // gerekli linq sorgularini yaparken unutma    
    }
}
