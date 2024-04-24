using Fiap.Domain.Commands.Class;
using Flunt.Validations;

namespace Fiap.Domain.Contracts.Class
{
    public class CreateClassContract : Contract<CreateClassCommand>
    {
        public CreateClassContract(CreateClassCommand command)
        {
            Requires()
                .IsNotNullOrEmpty(command.ClassName, "Nome da classe", "Nome da classe não pode ser nulo ou vazio.")
                .IsNotNullOrEmpty(command.Year.ToString(), "Ano", "O ano não pode ser nulo ou vazio.")
                .AreEquals(4, command.Year.ToString().Length, "Ano", "Ano precisa ter 4 caracteres.");
        }
    }
}