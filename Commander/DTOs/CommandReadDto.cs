namespace Commander.DTOs;

/* - Data transfer objects define how data will be sent back, in this case user should not get the id
 * - Each Dto needs a profile that maps the DTO to the Original Model
 */

public class CommandReadDto
{
    public int Id { get; set; }
    public string HowTo { get; set; }
    public string Line { get; set; }
    
    
}