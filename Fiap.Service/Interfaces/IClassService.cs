using Fiap.Domain.Commands;
using Fiap.Domain.Commands.Class;

namespace Fiap.Service.Interfaces
{
    public interface IClassService
    {
        Task<CommandResult> GetAllClass();
        Task<CommandResult> CreateClass(CreateClassCommand command);
        Task<CommandResult> UpdateClass(UpdateClassCommand command);
        Task<CommandResult> InactiveClass(Guid id);
    }
}
