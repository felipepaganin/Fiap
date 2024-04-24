using Fiap.Domain.Commands.Class;
using Fiap.Domain.Commands.Student;
using Fiap.Service.Interfaces;
using Fiap.Shared.Commands.Interfaces;
using Fiap.Shared.Handlers;

namespace Fiap.Service.Handlers
{
    public class CreateStudentHandler : IHandler<CreateStudentCommand>
    {
        private readonly IStudentService _studentService;

        public CreateStudentHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<ICommandResult> ExecuteCommand(CreateStudentCommand command)
        {
            return await _studentService.CreateStudent(command);
        }
    }
}