using Fiap.Domain.Contracts.Class;
using Fiap.Shared.Commands;

namespace Fiap.Domain.Commands.Class
{
    public class UpdateClassCommand : BaseCommand
    {
        public Guid IdExist { get; set; }
        public string ClassName { get; set; }
        public int Year { get; set; }

        public override void Validate()
        {
            AddNotifications(new UpdateClassContract(this));
        }

        public void InsertIdInCommand(Guid id)
        {
            IdExist = id;
        }
    }
}
