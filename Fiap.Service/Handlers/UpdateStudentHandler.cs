using Fiap.Domain.Commands.Student;
using Fiap.Service.Interfaces;
using Fiap.Shared.Commands.Interfaces;
using Fiap.Shared.Handlers;

namespace Fiap.Service.Handlers
{
    public class UpdateStudentHandler : IHandler<UpdateStudentCommand>
    {
        private readonly IStudentService _studentService;

        public UpdateStudentHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<ICommandResult> ExecuteCommand(UpdateStudentCommand command)
        {
            return await _studentService.UpdateStudent(command);
        }
    }
}