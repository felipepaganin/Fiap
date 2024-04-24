using Fiap.Domain.Contracts.Student;
using Fiap.Shared.Commands;

namespace Fiap.Domain.Commands.Student
{
    public class UpdateStudentCommand : BaseCommand
    {
        public Guid IdExist { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public override void Validate()
        {
            AddNotifications(new UpdateStudentContract(this));
        }

        public void InsertIdInCommand(Guid id)
        {
            IdExist = id;
        }
    }
}
