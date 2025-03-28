﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_system.DAL.Model
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        public int AppUserID { get; set; }

        [ForeignKey("AppUserID")]
        public AppUser AppUser { get; set; }
        public string Token { get; set; }
        public bool isRevoked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
}
