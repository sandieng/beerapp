using Microsoft.EntityFrameworkCore;

namespace BeerRosterAPI.Entities
{
    public class BeerRosterContext : DbContext
    {
        public BeerRosterContext(DbContextOptions<BeerRosterContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Roster> Rosters { get; set; }
    }
}
