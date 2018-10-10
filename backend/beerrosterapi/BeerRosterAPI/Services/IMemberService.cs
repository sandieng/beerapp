using BeerRosterAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BeerRosterAPI.Services
{
    public interface IMemberService
    {
        IOrderedQueryable<Member> GetAll();
        Member GetById(int id);
        Member GetByEmail(string email);
        void Save(Member member);
        void Update(Member member);
        void Delete(Member member);

        List<Member> GetNewMembers(List<Member> members, List<Roster> roster);
    }
}
