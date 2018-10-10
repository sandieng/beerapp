using System;
using System.Collections.Generic;

namespace BeerRosterAPI.Entities
{
    public class Member
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Roster> Rosters { get; set; }
    }
}
