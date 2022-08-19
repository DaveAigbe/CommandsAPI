using Commander.Models;

namespace Commander.Data;

/* - Interfaces are useful for decoupling data
 * - They are seen as contracts, with definitions that the implementing class must follow
 * - This decouples them because it can be used in multiple classes with different implementations
 * - Creates functions that the repository will need to implement
 * - "This is what each repository should have"
 */


public interface ICommanderRepo
{

    bool SaveChanges();
    
    // IEnumerable just iterates over a collection of a specific type, in this case, its the object Command
    IEnumerable<Command> GetAllCommands();
    Command GetCommandById(int id);
    // Use for creating new command, which will not have anything to return to the user
    void CreateCommand(Command cmd);

    void UpdateCommand(Command cmd);
    void DeleteCommand(Command cmd);
}