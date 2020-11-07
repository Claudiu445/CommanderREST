using CommanderREST.Models;
using Microsoft.EntityFrameworkCore;

namespace CommanderREST.Repository
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {
        }

        public DbSet<Command> Commands { get; set; }


    }
}
