using System;
using System.ComponentModel.DataAnnotations;

namespace BeerRosterAPI.ViewModels
{
    public class MemberVM 
    {
        public int ID { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime DateJoined { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
