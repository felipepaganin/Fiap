using Fiap.Domain.Commands.ClassStudent;
using Flunt.Validations;

namespace Fiap.Domain.Contracts.ClassStudent
{
    public class CreateClassStudentContract : Contract<CreateClassStudentCommand>
    {
        public CreateClassStudentContract(CreateClassStudentCommand command)
        {
            Requires()
                .IsNotNullOrEmpty(command.ClassId.ToString(), "ClassId", "ClassId não pode ser nulo ou vazio.")
                .IsNotNullOrEmpty(command.StudentId.ToString(), "StudentId", "StudentId não pode ser nulo ou vazio.");
        }
    }
}