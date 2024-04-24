using Fiap.Domain.Commands;
using Fiap.Domain.Commands.ClassStudent;

namespace Fiap.Service.Interfaces
{
    public interface IClassStudentService
    {
        Task<CommandResult> CreateClassStudent(CreateClassStudentCommand command);
        Task<CommandResult> GetAllClassStudent();
    }
}
