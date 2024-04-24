using Fiap.Domain.Commands.Class;
using Fiap.Service.Interfaces;
using Fiap.Shared.Commands.Interfaces;
using Fiap.Shared.Handlers;

namespace Fiap.Service.Handlers
{
    public class UpdateClassHandler : IHandler<UpdateClassCommand>
    {
        private readonly IClassService _classService;

        public UpdateClassHandler(IClassService classService)
        {
            _classService = classService;
        }

        public async Task<ICommandResult> ExecuteCommand(UpdateClassCommand command)
        {
            return await _classService.UpdateClass(command);
        }
    }
}
