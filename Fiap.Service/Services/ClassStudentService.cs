using Fiap.Data.Interfaces;
using Fiap.Domain.Commands.Student;
using Fiap.Domain.Commands;
using Fiap.Domain.Entities;
using Fiap.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiap.Domain.Commands.ClassStudent;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Service.Services
{
    public class ClassStudentService : IClassStudentService
    {
        private readonly IReadRepository<ClassStudent> _readRepository;
        private readonly IWriteRepository<ClassStudent> _writeRepository;
        private readonly IReadRepository<Student> _studentReadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClassStudentService(IReadRepository<ClassStudent> readRepository, IWriteRepository<ClassStudent> writeRepository, IUnitOfWork unitOfWork, IReadRepository<Student> studentReadRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
            _studentReadRepository = studentReadRepository;
        }

        public async Task<CommandResult> GetAllClassStudent()
        {
            var classStudentList = await _readRepository.FindAll().ToListAsync();

            if (classStudentList is null || classStudentList.Count == 0)
                return new CommandResult(false, "Falha ao consultar turmas e alunos");

            return new CommandResult(true, "Turmas e alunos consultados com sucesso", classStudentList);
        }

        public async Task<CommandResult> CreateClassStudent(CreateClassStudentCommand command)
        {
            command.Validate();

            var notifications = command.Notifications;

            var existingStudent = _studentReadRepository.FindByCondition(x => x.Id == command.StudentId);

            if (existingStudent != null)
            {
                return new CommandResult(false, "O aluno já está associado à turma", notifications);
            }

            if (!command.IsValid)
                return new CommandResult(false, "Falha ao vincular aluno e turma", notifications);

            ClassStudent createClassStudent = new ClassStudent(command.ClassId, command.StudentId);

            await _writeRepository.AddAsync(createClassStudent);
            await _unitOfWork.CommitAsync();

            return new CommandResult(true, "Aluno registrado com sucesso", createClassStudent);
        }
    }
}