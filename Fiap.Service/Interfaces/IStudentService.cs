using Fiap.Domain.Commands.Class;
using Fiap.Domain.Commands;
using Fiap.Domain.Commands.Student;

namespace Fiap.Service.Interfaces
{
    public interface IStudentService
    {
        Task<CommandResult> GetAllStudents();
        Task<CommandResult> CreateStudent(CreateStudentCommand command);
        Task<CommandResult> UpdateStudent(UpdateStudentCommand command);
        Task<CommandResult> InactiveStudent(Guid id);
    }
}
