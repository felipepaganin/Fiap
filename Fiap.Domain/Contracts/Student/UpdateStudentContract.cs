﻿using Fiap.Domain.Commands.Student;
using Flunt.Validations;

namespace Fiap.Domain.Contracts.Student
{
    public class UpdateStudentContract : Contract<UpdateStudentCommand>
    {
        public UpdateStudentContract(UpdateStudentCommand command)
        {
            Requires()
                .IsNotNullOrEmpty(command.Name, "Nome", "Nome do aluno não pode ser nulo ou vazio.")
                .IsGreaterOrEqualsThan(255, command.Name.Length, "Nome", "Nome do aluno não pode ser superior a 255 caracteres.")
                .IsNotNullOrEmpty(command.User, "Usuario", "Usuario do aluno não pode ser nulo ou vazio.")
                .IsGreaterOrEqualsThan(45, command.User.Length, "Usuario", "Usuario do aluno não pode ser superior a 45 caracteres.")
                .IsNotNullOrEmpty(command.Password, "Senha", "Senha do aluno não pode ser nulo ou vazio.")
                .IsGreaterOrEqualsThan(60, command.Password.Length, "Senha", "Senha do aluno não pode ser superior a 60 caracteres.");
        }
    }
}