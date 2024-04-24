using Fiap.Shared.Commands.Interfaces;
using Flunt.Notifications;

namespace Fiap.Shared.Commands
{
    public abstract class BaseCommand : Notifiable<Notification>, ICommand
    {
        public abstract void Validate();
    }
}