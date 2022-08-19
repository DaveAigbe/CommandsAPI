using Commander.Models;

namespace Commander.Data;
/* - Mock repository is used for testing calls and functionality
 * - Does not connect to anything, just has hard coded data, which will be expected
 * - This shows one importance of interface, these functions are also implemented in SqlCommanderRepo, but in a diff way
 */



// Current class implements the interface(contract) 
public class MockCommanderRepo : ICommanderRepo
{
    // Return type is an enumerable type, in this case a list
    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Command> GetAllCommands()
    {
        var commands = new List<Command>
        {
            new Command { Id = 0, HowTo = "Boil an egg", Line = "Boiling", Platform = "Kettle and Pan" },
            new Command { Id = 1, HowTo = "Cut bread", Line = "Cutting", Platform = "Cutting board" },
            new Command { Id = 2, HowTo = "Brew Coffee", Line = "Brewing", Platform = "Stovetop" }
        };

        return commands;

    }

    public Command GetCommandById(int id)
    {
        return new Command{Id= 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle and Pan"};
    }

    public void CreateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public void UpdateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public void DeleteCommand(Command cmd)
    {
        throw new NotImplementedException();
    }
}