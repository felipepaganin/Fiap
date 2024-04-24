using Fiap.Data.Interfaces;
using Fiap.Domain.Commands;
using Fiap.Domain.Commands.Class;
using Fiap.Domain.Entities;
using Fiap.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Service.Services
{
    public class ClassService : IClassService
    {
        private readonly IReadRepository<Class> _readRepository;
        private readonly IWriteRepository<Class> _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClassService(IReadRepository<Class> readRepository, IWriteRepository<Class> writeRepository, IUnitOfWork unitOfWork)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> GetAllClass()
        {
            var classList = await _readRepository.FindAll().ToListAsync();

            if (classList is null || classList.Count == 0)
                return new CommandResult(false, "Falha ao consultar turmas");

            return new CommandResult(true, "Turmas consultados com sucesso", classList);
        }

        public async Task<CommandResult> CreateClass(CreateClassCommand command)
        {
            command.Validate();

            var notifications = command.Notifications.Concat(command.Notifications);

            if (!command.IsValid)
                return new CommandResult(false, "Falha ao registrar trumas", notifications);

            Class createClass = new Class(command.ClassName, command.Year) ;

            await _writeRepository.AddAsync(createClass);
            await _unitOfWork.CommitAsync();

            return new CommandResult(true, "Turma registrado com sucesso", createClass);
        }

        public async Task<CommandResult> UpdateClass(UpdateClassCommand command)
        {
            command.Validate();

            var notifications = command.Notifications.Concat(command.Notifications);

            if (!command.IsValid)
                return new CommandResult(false, "Falha ao alterar turma", notifications);

            var classEdit = await _readRepository.FindByCondition(x => x.Id == command.IdExist).FirstOrDefaultAsync();
            if (classEdit is null)
                return new CommandResult(false, "Falha ao recuperar turma");


            classEdit.Update(command);

            _writeRepository.Update(classEdit);
            await _unitOfWork.CommitAsync();

            return new CommandResult(true, "Cliente alterado com sucesso", classEdit);
        }

        public async Task<CommandResult> InactiveClass(Guid id)
        {
            var inactiveClass = await _readRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
            if (inactiveClass is null)
                return new CommandResult(false, "Falha ao recuperar turma");

            inactiveClass.Inactive();
            _writeRepository.Update(inactiveClass);
            await _unitOfWork.CommitAsync();

            return new CommandResult(true, "Cliente deletado com sucesso", inactiveClass);
        }
    }
}
