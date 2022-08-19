﻿using AutoMapper;
using Commander.Data;
using Commander.DTOs;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers;
/* - Controllers work at a resource level
 * - Control requests and actions
 * - Controller will ask for ICommanderRepo at runtime, which will return the SqlCommanderRepo
 *   ,that repository is used to return info from the database and uses them in the endpoints
 * - "Hey pass me(Inject) the proper class(which grabs data from db) that implements ICommanderRepo
 *    so i can use it to return the data in a http request"
 */


// How do you get to the endpoints and resources from your controller
// api/[controller] can also be used, if controller name will change
[Route("api/commands")]
// Gives default behaviour for API
[ApiController]

// ControllerBase provides MVC handling, Base because it has no view otherwise it would be 'Controller'
public class CommandsController : ControllerBase
{
    // Create an object of the repository so that the ActionResults(result from requests) have access to the data
    // private readonly MockCommanderRepo _repository = new MockCommanderRepo();
    private readonly ICommanderRepo _repository;
    private readonly IMapper _mapper;

    // Pass through a class that has implemented the repository, set the Controllers repository to that
    // Pass through IMapper mapper which will send over the dto information instead of the raw data
    public CommandsController(ICommanderRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    // Endpoints
    
    // GET api/commands
    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
    {
        var commandItems = _repository.GetAllCommands();
        
        // Creates an OkObjectResult object that produces an Status200OK response
        // Mapping must use an IEnumerable in this case because that is what is being returned/stored
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
    }
    
    // GET api/commands/id
    [HttpGet("{id}", Name = "GetCommandById")]
    // Return the DTO 
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
        var commandItem = _repository.GetCommandById(id);
        if (commandItem != null)
        {
            // use mapper to return the dto version(specified by CommandReadDto) of the original object that store
            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        return NotFound();
    }
    
    // POST api/commands
    [HttpPost]
    // Return a ReadDto to show the updated object
    // CreateDto as a parameter because the client should not be inputting id, that is auto generated by database
    // Post methods must pass back the resource that was created AND the location(URI)
    public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
    {
        // _mapper.Map<create_this>(using this data)
        // This is important because the database understands Command objects, so that is what must be passed to it
        var commandModel = _mapper.Map<Command>(commandCreateDto);
        _repository.CreateCommand(commandModel);
        
        // Persist changes down to database
        _repository.SaveChanges();
        
        // Client should not see id, so return the dto of the newly created object
        var commandItem = _mapper.Map<CommandReadDto>(commandModel);
        
        // 1. The name of the route to use for generating the URL(Name of the function that finds specific routes by id)
        // 2. The route data to use for generating the URL.(Found by id)
        // 3. The content that will be displayed when route is called
        return CreatedAtRoute(nameof(GetCommandById), new { Id = commandItem.Id }, commandItem);
    }
    
    // PUT api/commands/id
    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
    {
        // Get the repository that will be updated and check there is a resource to update
        var commandModelFromRepo = _repository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
            return NotFound();
        }
        
        // Execute a mapping(Swap) from the source object to the existing destination object
        _mapper.Map(commandUpdateDto, commandModelFromRepo);
        
        // Currently does nothing but if implementation were to switch, would have access to the command being changed
        _repository.UpdateCommand(commandModelFromRepo);

        _repository.SaveChanges();

        return NoContent();
    }
    
    // PATCH api/commands/id
    [HttpPatch("{id}")]
    public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDocument)
    {
        var commandModelFromRepo = _repository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
            return NotFound();
        }
        
        // Create new commandUpdateDto with the specified command(id) content
        var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
        
        // Apply patches to the new CommandUpdateDto object
        patchDocument.ApplyTo(commandToPatch);
        // test to see if the document cannot be changed throw error
        if (!TryValidateModel(commandToPatch))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(commandToPatch, commandModelFromRepo);
        _repository.SaveChanges();

        return NoContent();

    }
    
    // DELETE api/commands/id
    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id)
    {
        var commandModelFromRepo = _repository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
            return NotFound();
        }
        
        _repository.DeleteCommand(commandModelFromRepo);
        _repository.SaveChanges();

        return NoContent();
    }




}