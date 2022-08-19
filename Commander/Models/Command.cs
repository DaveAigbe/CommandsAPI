using System.ComponentModel.DataAnnotations;

namespace Commander.Models;

/* Setup fields that will be used in the database
 * Each Model must have a Id variable that translates to each elements unique key
 */

public class Command
{
    // Specific id for command
    [Key]
    public int Id { get; set; }
    // How to implement command
    [Required]
    [MaxLength(250)]
    public string HowTo { get; set; }
    // Command line snippet
    [Required]
    public string Line { get; set; }
    // Platform that that cmd line relates to
    [Required]
    public string Platform { get; set; }
    
}