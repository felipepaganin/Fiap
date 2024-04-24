using Fiap.Domain.Commands;
using Fiap.Domain.Commands.Student;
using Fiap.Service.Interfaces;
using Fiap.Shared.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IHandler<CreateStudentCommand> _createStudentHandler;
        private readonly IHandler<UpdateStudentCommand> _updateStudentHandler;


        public StudentController(IStudentService studentService, IHandler<CreateStudentCommand> createStudentHandler, IHandler<UpdateStudentCommand> updateStudentHandler)
        {
            _studentService = studentService;
            _createStudentHandler = createStudentHandler;
            _updateStudentHandler = updateStudentHandler;
        }

        [HttpGet("ListStudents")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _studentService.GetAllStudents();

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("CreateStudent")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(CreateStudentCommand command)
        {
            var result = await _createStudentHandler.ExecuteCommand(command);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAsync(Guid id, UpdateStudentCommand command)
        {
            command.InsertIdInCommand(id);
            var result = await _updateStudentHandler.ExecuteCommand(command);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("inactive/{id}")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _studentService.InactiveStudent(id);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
