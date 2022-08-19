using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data;

/* - Returns data from the database
 * - Creates Tables using the specified data(Model)
 * - "Hey model how should i set up the Database?"
 */

public class CommanderDbContext : DbContext
{
    // For DbContext to work it needs the DbContextOptions class
    // We want the options to be applied to our current class, so the that will be in the type options
    public CommanderDbContext(DbContextOptions<CommanderDbContext> opt) 
        : base(opt)
    {
        
    }
    
    // DbSet to modify table data, in this case table full of 'Command' objects
    public DbSet<Command> Commands { get; set; }


}