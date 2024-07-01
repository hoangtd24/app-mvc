using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace aspnetcoremvc.Models
{

    public class AppUser : IdentityUser
    {

        [MaxLength(100)]
        public string FullName { set; get; } = String.Empty;

        [MaxLength(255)]
        public string Address { set; get; } = String.Empty;

        [DataType(DataType.Date)]
        public DateTime? Birthday { set; get; }

    }
}