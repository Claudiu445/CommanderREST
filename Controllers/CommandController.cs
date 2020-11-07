using AutoMapper;
using CommanderREST.Dtos;
using CommanderREST.Models;
using CommanderREST.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CommanderREST.Controllers
{
    [Route("restapi/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommanderRepo _repository;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        //Get restapi/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandDto>>(commandItems));
        }

        //Get restapi/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
                return Ok(_mapper.Map<CommandDto>(commandItem));
            else
                return NotFound();
        }

        //Post restapi/commands
        [HttpPost]
        public ActionResult<CommandDto> CreateCommand(CommandDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadModel = _mapper.Map<CommandDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandModel.Id }, commandReadModel);
        }

        //Put restapi/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        //Patch restapi/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandDto> jsonPatchDocument)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandDto>(commandModelFromRepo);

            jsonPatchDocument.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();
            return NoContent();
        }
        //Delete restapi/commands/{id}
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
}