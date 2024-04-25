using Fiap.Data.Interfaces;
using Fiap.Domain.Commands;
using Fiap.Domain.Commands.Class;
using Fiap.Domain.Entities;
using Fiap.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            var classList = await _readRepository.FindAllAsync();

            if (classList.IsNullOrEmpty())
                return new CommandResult(false, "Falha ao consultar turmas");

            return new CommandResult(true, "Turmas consultados com sucesso", classList);
        }

        public async Task<CommandResult> CreateClass(CreateClassCommand command)
        {
            command.Validate();

            var notifications = command.Notifications;

            var existingClassName = _readRepository.FindByConditionAsync(x => x.ClassName == command.ClassName).Result;

            var actualDate = DateTime.Now.Year;

            if(command.Year < actualDate)
                return new CommandResult(false, "Não pode cadastrar turma com data menor que a atual", notifications);

            if (existingClassName != null)
                return new CommandResult(false, "Ja existe turma com esse nome cadastrada", notifications);

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

            var notifications = command.Notifications;

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
            var inactiveClass = await _readRepository.FindByConditionAsync(x => x.Id == id);
            if (inactiveClass is null)
                return new CommandResult(false, "Falha ao recuperar turma");

            inactiveClass.Inactive();
            _writeRepository.Update(inactiveClass);
            await _unitOfWork.CommitAsync();

            return new CommandResult(true, "Cliente deletado com sucesso", inactiveClass);
        }
    }
}
