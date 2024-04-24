using Fiap.Domain.Commands.Class;
using Fiap.Service.Interfaces;
using Fiap.Shared.Commands.Interfaces;
using Fiap.Shared.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Service.Handlers
{
    public class CreateClassHandler : IHandler<CreateClassCommand>
    {
        private readonly IClassService _classService;

        public CreateClassHandler(IClassService classService)
        {
            _classService = classService;
        }

        public async Task<ICommandResult> ExecuteCommand(CreateClassCommand command)
        {
            return await _classService.CreateClass(command);
        }
    }
}
