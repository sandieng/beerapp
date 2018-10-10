using BeerRosterAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BeerRosterAPI.Services
{
    public interface IRosterService
    {
        IOrderedQueryable<Roster> GetAll();
        Roster GetById(int id);
        Roster GetByEmail(string email);
        void Save(Roster roster);
        void Save(List<Roster> roster);
        void Update(Roster roster);
        void Delete(Roster roster);
    }
}
