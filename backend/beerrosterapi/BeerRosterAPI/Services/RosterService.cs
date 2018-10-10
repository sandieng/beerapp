using System;
using System.Collections.Generic;
using System.Linq;
using BeerRosterAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeerRosterAPI.Services
{
    public class RosterService : IRosterService
    {
        private BeerRosterContext _context;
        private IBeerRosterRepository<Roster> _repository;

        public RosterService(BeerRosterContext context, IBeerRosterRepository<Roster> repository)
        {
            _context = context;
            _repository = repository;
        }

        public void Delete(Roster roster)
        {
            _repository.Delete(roster);
        }

        public IOrderedQueryable<Roster> GetAll()
        {
            //return _repository.GetAll().OrderBy(m => m.FirstName);
            //return _repository.GetAll().OrderBy(m => m.RosteredDate);

            // Only include active roster
            var x =_context.Rosters.Include(r => r.Member).Where(r => r.MemberID == r.Member.ID && r.RosteredDate.Date >= DateTime.Now.Date);
            return x.OrderBy(o => o.RosteredDate);
        }

        public Roster GetByEmail(string email)
        {
            //return _repository.Single(x => x.Email == email);
            return null;
        }

        public Roster GetById(int id)
        {
            //return _repository.GetById(id);
            return _repository.Single(x => x.ID == id);
        }

        public void Save(Roster member)
        {
            _repository.Save(member);
        }

        public void Save(List<Roster> roster)
        {
            foreach (var item in roster)
            {
                _repository.Save(item);
            }
        }


        public void Update(Roster roster)
        {
            _repository.Update(roster);
        }
    }
}
