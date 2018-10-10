using BeerRosterAPI.Entities;
using System;
using System.Collections.Generic;

namespace BeerRosterAPI.BusinessLayer
{
    public class Scheduler
    {
        public List<Roster> CreateRoster(List<Member> activeMembers, DateTime lastRosteredDate, int numberOfCycles = 1)
        {
            var beerRoster = new List<Roster>();

            if (lastRosteredDate != DateTime.MinValue)
            {
                lastRosteredDate = lastRosteredDate.AddDays(7);
            }

            var comingFriday = (lastRosteredDate == DateTime.MinValue ? NextFriday(DateTime.Now) : lastRosteredDate);
            for (int i = 0; i < numberOfCycles; i++)
            {
                foreach (var member in activeMembers)
                {
                    var roster = new Roster
                    {
                        MemberID = member.ID,
                        Member = member,
                        RosteredDate = comingFriday
                    };

                    beerRoster.Add(roster);
                    comingFriday = comingFriday.AddDays(7);
                }
            }

            return beerRoster;
        }

        public DateTime NextFriday(DateTime currentDate)
        {
            for (int i = 0; i < 7; i++)
            {
                if (currentDate.DayOfWeek == DayOfWeek.Friday)
                    return currentDate;
                else
                    currentDate = currentDate.AddDays(1);
            }

            return currentDate;
        }
    }
}
