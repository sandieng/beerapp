using System.Collections.Generic;
using System.Linq;
using BeerRosterAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeerRosterAPI.Services
{
    public class MemberService : IMemberService
    {
        private BeerRosterContext _context;
        private IBeerRosterRepository<Member> _repository;

        public MemberService(BeerRosterContext context,  IBeerRosterRepository<Member> repository)
        {
            _context = context;
            _repository = repository;
        }

        public IOrderedQueryable<Member> GetAll()
        {
            var result = _context.Members.Include(m => m.Rosters);
            return _repository.GetAll().OrderBy(m => m.DateJoined);
        }

        public Member GetByEmail(string email)
        {
            return _repository.Single(x => x.Email == email);
        }

        public Member GetById(int id)
        {
            return _repository.Single(x => x.ID == id);
        }

        public void Save(Member member)
        {
            _repository.Save(member);
        }

        public void Update(Member member)
        {
            _repository.Update(member);
        }

        public List<Member> GetNewMembers(List<Member> members, List<Roster> roster)
        {
            // Eliminate old members from new members
            var newMembers = new List<Member>();
            foreach (var member in members)
            {
                var found = roster.FirstOrDefault(r => r.MemberID == member.ID);
                if (found != null) continue;

                newMembers.Add(member);
            }

            return newMembers;
        }

        public void Delete(Member member)
        {
            Update(member);
        }
    }
}
