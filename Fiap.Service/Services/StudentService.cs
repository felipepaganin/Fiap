using Fiap.Data.Interfaces;
using Fiap.Domain.Commands;
using Fiap.Domain.Commands.Student;
using Fiap.Domain.Entities;
using Fiap.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Fiap.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IReadRepository<Student> _readRepository;
        private readonly IWriteRepository<Student> _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IReadRepository<Student> readRepository, IWriteRepository<Student> writeRepository, IUnitOfWork unitOfWork)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> GetAllStudents()
        {
            var students = await _readRepository.FindAllAsync();

            if (students.IsNullOrEmpty())
                return new CommandResult(false, "Falha ao consultar alunos");

            return new CommandResult(true, "Alunos consultados com sucesso", students);
        }

        public async Task<CommandResult> CreateStudent(CreateStudentCommand command)
        {
            command.Validate();

            var notifications = command.Notifications;

            if (!command.IsValid)
                return new CommandResult(false, "Falha ao registrar Aluno", notifications);

            Student createSudent = new Student(command.Name, command.User, command.Password);

            await _writeRepository.AddAsync(createSudent);
            await _unitOfWork.CommitAsync();

            return new CommandResult(true, "Aluno registrado com sucesso", createSudent);
        }

        public async Task<CommandResult> UpdateStudent(UpdateStudentCommand command)
        {
            command.Validate();

            var notifications = command.Notifications;

            if (!command.IsValid)
                return new CommandResult(false, "Falha ao alterar aluno", notifications);

            var student = await _readRepository.FindByCondition(x => x.Id == command.IdExist).FirstOrDefaultAsync();
            if (student is null)
                return new CommandResult(false, "Falha ao recuperar aluno");


            student.Update(command);

            _writeRepository.Update(student);
            await _unitOfWork.CommitAsync();

            return new CommandResult(true, "Aluno alterado com sucesso", student);
        }

        public async Task<CommandResult> InactiveStudent(Guid id)
        {
            var student = await _readRepository.FindByConditionAsync(x => x.Id == id);
            if (student is null)
                return new CommandResult(false, "Falha ao recuperar aluno");

            student.Inactive();
            _writeRepository.Update(student);
            await _unitOfWork.CommitAsync();

            return new CommandResult(true, "Aluno deletado com sucesso", student);
        }
    }
}