using Fiap.Domain.Commands.ClassStudent;
using Fiap.Service.Interfaces;
using Fiap.Shared.Commands.Interfaces;
using Fiap.Shared.Handlers;

namespace Fiap.Service.Handlers
{
    public class CreateClassStudentHandler : IHandler<CreateClassStudentCommand>
    {
        private readonly IClassStudentService _classStudentService;

        public CreateClassStudentHandler(IClassStudentService classStudentService)
        {
            _classStudentService = classStudentService;
        }

        public async Task<ICommandResult> ExecuteCommand(CreateClassStudentCommand command)
        {
            return await _classStudentService.CreateClassStudent(command);
        }
    }
}
