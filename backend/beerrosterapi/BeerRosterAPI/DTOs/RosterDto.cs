using System;

namespace BeerRosterAPI.DTOs
{
    public class RosterDto
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public DateTime RosteredDate { get; set; }
    }
}
