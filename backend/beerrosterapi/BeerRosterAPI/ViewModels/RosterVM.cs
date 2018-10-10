using System;

namespace BeerRosterAPI.ViewModels
{
    public class RosterVM
    {
        public int ID { get; set; }
        public DateTime RosteredDate { get; set; }

        public int MemberID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsActive { get; set; } 
    }
}
