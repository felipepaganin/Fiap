using Fiap.Data.Interfaces;
using Fiap.Domain.Commands.Class;
using Fiap.Domain.Entities;
using Fiap.Service.Services;
using Moq;
using System.Linq.Expressions;

namespace Fiap.Test.Service.Services
{
    public class ClassServiceTests
    {
        private readonly Mock<IReadRepository<Class>> _mockReadRepository = new Mock<IReadRepository<Class>>();
        private readonly Mock<IWriteRepository<Class>> _mockWriteRepository = new Mock<IWriteRepository<Class>>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly ClassService _service;

        public ClassServiceTests()
        {
            _service = new ClassService(_mockReadRepository.Object, _mockWriteRepository.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllClass_ReturnsSuccess_WhenClassesExist()
        {
            // Arrange
            var classes = new List<Class> { new Class("Math", 2022) };
            _mockReadRepository.Setup(repo => repo.FindAllAsync()).ReturnsAsync(classes.AsQueryable());

            // Act
            var result = await _service.GetAllClass();

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Turmas consultados com sucesso", result.Message);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetAllClass_ReturnsFailure_WhenNoClassesFound()
        {
            // Arrange
            _mockReadRepository.Setup(repo => repo.FindAllAsync()).ReturnsAsync(new List<Class>().AsQueryable());

            // Act
            var result = await _service.GetAllClass();

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Falha ao consultar turmas", result.Message);
        }

        [Fact]
        public async Task CreateClass_ReturnsFailure_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new CreateClassCommand { ClassName = "Math", Year = 2023 };
            command.Validate();

            _mockReadRepository.Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<Class, bool>>>()))
                         .Returns(new List<Class>().AsQueryable());

            // Act
            var result = await _service.CreateClass(command);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Não pode cadastrar turma com data menor que a atual", result.Message);
        }

        [Fact]
        public async Task CreateClass_ReturnsFailure_WhenYearLessThanCurrent()
        {
            // Arrange
            var command = new CreateClassCommand { ClassName = "Math", Year = DateTime.Now.Year - 1 };
            _mockReadRepository.Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<Class, bool>>>()))
                         .Returns(new List<Class>().AsQueryable());

            // Act
            var result = await _service.CreateClass(command);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Não pode cadastrar turma com data menor que a atual", result.Message);
        }

        [Fact]
        public async Task CreateClass_ReturnsSuccess_WhenValidClassIsCreated()
        {
            // Arrange
            var command = new CreateClassCommand { ClassName = "Math", Year = DateTime.Now.Year };
            _mockReadRepository.Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<Class, bool>>>()))
                         .Returns(new List<Class>().AsQueryable());
            _mockWriteRepository.Setup(repo => repo.AddAsync(It.IsAny<Class>())).Returns(Task.FromResult(new Class()));
            _mockWriteRepository.Setup(repo => repo.AddAsync(It.IsAny<Class>()))
                .Returns(Task.FromResult(new Class()));

            // Act
            var result = await _service.CreateClass(command);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Turma registrado com sucesso", result.Message);
        }


        [Fact]
        public async Task UpdateClass_ReturnsFailure_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new UpdateClassCommand { IdExist = Guid.NewGuid() };
            command.Validate();

            // Act
            var result = await _service.UpdateClass(command);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Falha ao alterar turma", result.Message);
        }

        [Fact]
        public async Task UpdateClass_ReturnsFailure_WhenClassDoesNotExist()
        {
            // Arrange
            var command = new UpdateClassCommand { IdExist = Guid.NewGuid() };
            _mockReadRepository.Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<Class, bool>>>()))
                         .Returns(new List<Class>().AsQueryable());

            // Act
            var result = await _service.UpdateClass(command);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Falha ao alterar turma", result.Message);
        }

        [Fact]
        public async Task InactiveClass_ReturnsFailure_WhenClassDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockReadRepository.Setup(repo => repo.FindByCondition(x => x.Id == id))
                         .Returns(new List<Class>().AsQueryable());

            // Act
            var result = await _service.InactiveClass(id);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Falha ao recuperar turma", result.Message);
        }
    }
}