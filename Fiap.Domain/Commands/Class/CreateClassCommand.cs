using Fiap.Domain.Contracts.Class;
using Fiap.Shared.Commands;

namespace Fiap.Domain.Commands.Class
{
    public class CreateClassCommand : BaseCommand
    {
        public string ClassName { get; set; }
        public int Year { get; set; }
        public override void Validate()
        {
            AddNotifications(new CreateClassContract(this));
        }
    }
}
