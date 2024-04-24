using Fiap.Domain.Commands;
using Fiap.Domain.Commands.Class;
using Fiap.Service.Interfaces;
using Fiap.Shared.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly IHandler<CreateClassCommand> _createClassHandler;
        private readonly IHandler<UpdateClassCommand> _updateClassHandler;


        public ClassController(IClassService classService, IHandler<CreateClassCommand> createClassHandler, IHandler<UpdateClassCommand> updateClassHandler)
        {
            _classService = classService;
            _createClassHandler = createClassHandler;
            _updateClassHandler = updateClassHandler;
        }

        [HttpGet("ListClass")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _classService.GetAllClass();

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("CreateClass")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(CreateClassCommand command)
        {
            var result = await _createClassHandler.ExecuteCommand(command);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAsync(Guid id, UpdateClassCommand command)
        {
            command.InsertIdInCommand(id);
            var result = await _updateClassHandler.ExecuteCommand(command);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("inactive/{id}")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _classService.InactiveClass(id);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
