using Fiap.Domain.Contracts.ClassStudent;
using Fiap.Shared.Commands;

namespace Fiap.Domain.Commands.ClassStudent
{
    public class CreateClassStudentCommand : BaseCommand
    {
        public Guid ClassId { get; set; }
        public Guid StudentId { get; set; }
        public override void Validate()
        {
            AddNotifications(new CreateClassStudentContract(this));
        }
    }
}