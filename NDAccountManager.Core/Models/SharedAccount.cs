﻿namespace NDAccountManager.Core.Models
{
    public class SharedAccount
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public bool IsUnlimited { get; set; }
        public DateTime? ExpirationDate { get; set; }

    }
}
