using System;

namespace BeerRosterAPI.Entities
{
    public class Roster
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public DateTime RosteredDate { get; set; }

        public virtual Member Member { get; set; }
    }
}
