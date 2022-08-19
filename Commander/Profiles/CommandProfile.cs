using AutoMapper;
using Commander.DTOs;
using Commander.Models;

namespace Commander.Profiles;

/* - Each Model/Dto profile inherits from 'Profile', indicating that it is a profile
 * - Has a constructor with the function CreateMap which indicates the <Model, DTO> combination
 * 
 */


public class CommandProfile : Profile
{
    public CommandProfile()
    {
        // Source -> Target
        CreateMap<Command, CommandReadDto>();
        // The CreateDto object, will be made into a command 
        CreateMap<CommandCreateDto, Command>();
        CreateMap<CommandUpdateDto, Command>();
        CreateMap<Command, CommandUpdateDto>();
    }
}