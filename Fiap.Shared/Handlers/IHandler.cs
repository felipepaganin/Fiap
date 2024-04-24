using Fiap.Shared.Commands.Interfaces;

namespace Fiap.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> ExecuteCommand(T command);
    }
}