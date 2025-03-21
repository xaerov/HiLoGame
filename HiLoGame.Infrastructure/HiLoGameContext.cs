using HiLoGame.Domain;
using Microsoft.EntityFrameworkCore;

namespace HiLoGame.Infrastructure
{
    public class HiLoGameContext : DbContext
    {
        public HiLoGameContext(DbContextOptions<HiLoGameContext> options) : base(options)
        { }

        public DbSet<Game> Games { get; set; }
    }
}
