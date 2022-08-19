using System.Runtime.CompilerServices;
using Commander.Models;

namespace Commander.Data;

/* - Implements the interface so it can be used for dependency injection in the Controller
 * - Injects the DbContext object, which will return the data from the database
 * - "Hey DbContext give me(Inject) the database data so i can use it in my implemented functions"
 */

public class SqlCommanderRepo : ICommanderRepo
{
    // Create an object that will grab info from DbContext
    private readonly CommanderDbContext _context;
    
    // Use dependency injection to insert the DbContext object when it is being used
    public SqlCommanderRepo(CommanderDbContext context)
    {
        _context = context;
    }

    // This is to ensure that when changes are made via the dbcontext, they are reflected on the actual database
    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }

    public IEnumerable<Command> GetAllCommands()
    {
        return _context.Commands.ToList();
    }

    public Command GetCommandById(int id)
    {
        return _context.Commands.FirstOrDefault((p) => p.Id == id);
    }

    public void CreateCommand(Command cmd)
    {
        // If the cmd object sent through is empty, alert user
        if (cmd == null)
        {
            throw new ArgumentNullException();
        }
        // Add the command to the Commands DbSet
        _context.Commands.Add(cmd);
    }

    public void UpdateCommand(Command cmd)
    {
        // Nothing
        
    }

    public void DeleteCommand(Command cmd)
    {
        if (cmd == null)
        {
            throw new ArgumentNullException();
        }

        _context.Commands.Remove(cmd);
    }
}