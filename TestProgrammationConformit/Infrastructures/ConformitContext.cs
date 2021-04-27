using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Entities;

namespace TestProgrammationConformit.Infrastructures
{
    public class ConformitContext : DbContext
    {
        public ConformitContext(DbContextOptions<ConformitContext> options) : base(options)
        {
        }

        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<EvenementComment> EvenementComments { get; set; }
    }
}
