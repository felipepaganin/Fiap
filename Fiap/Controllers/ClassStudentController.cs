using Fiap.Domain.Commands;
using Fiap.Domain.Commands.ClassStudent;
using Fiap.Service.Interfaces;
using Fiap.Service.Services;
using Fiap.Shared.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassStudentController : ControllerBase
    {
        private readonly IClassStudentService _ClassStudentService;
        private readonly IHandler<CreateClassStudentCommand> _createClassStudentHandler;
        //private readonly IHandler<UpdateClassStudentCommand> _updateStudentHandler;


        public ClassStudentController(IClassStudentService classStudentService, IHandler<CreateClassStudentCommand> createClassStudentHandler)
        {
            _ClassStudentService = classStudentService;
            _createClassStudentHandler = createClassStudentHandler;
        }

        [HttpGet("ListClassStudent")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _ClassStudentService.GetAllClassStudent();

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("CreateClassStudent")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(CreateClassStudentCommand command)
        {
            var result = await _createClassStudentHandler.ExecuteCommand(command);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
