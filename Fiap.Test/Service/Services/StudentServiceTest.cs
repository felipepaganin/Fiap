using Fiap.Data.Interfaces;
using Fiap.Domain.Commands.Student;
using Fiap.Domain.Entities;
using Fiap.Service.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Test.Service.Services
{
    public class StudentServiceTest
    {
        private readonly Mock<IReadRepository<Student>> _mockReadRepository = new Mock<IReadRepository<Student>>();
        private readonly Mock<IWriteRepository<Student>> _mockWriteRepository = new Mock<IWriteRepository<Student>>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly StudentService _service;

        public StudentServiceTest()
        {
            _service = new StudentService(_mockReadRepository.Object, _mockWriteRepository.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllStudents_ReturnsSuccess_WhenStudentsExist()
        {
            // Arrange
            var students = new List<Student> { new Student("Felipe", "aasdasd", "teste") };
            _mockReadRepository.Setup(repo => repo.FindAllAsync()).ReturnsAsync(students.AsQueryable());

            // Act
            var result = await _service.GetAllStudents();

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Alunos consultados com sucesso", result.Message);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetAllStudents_ReturnsFailure_WhenNoStudentsFound()
        {
            // Arrange
            _mockReadRepository.Setup(repo => repo.FindAllAsync()).ReturnsAsync(new List<Student>().AsQueryable());

            // Act
            var result = await _service.GetAllStudents();

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Falha ao consultar alunos", result.Message);
        }

        [Fact]
        public async Task CreateStudent_ReturnsFailure_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new CreateStudentCommand { Name = "Felipe", User = "paganin", Password = "asdasd1" };
            command.Validate();

            _mockReadRepository.Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<Student, bool>>>()))
                         .Returns(new List<Student>().AsQueryable());

            // Act
            var result = await _service.CreateStudent(command);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Falha ao registrar Aluno", result.Message);
        }

        [Fact]
        public async Task CreateStudent_ReturnsSuccess_WhenValidStudentIsCreated()
        {
            // Arrange
            var command = new CreateStudentCommand { Name = "Felipe", User = "paganin", Password = "a123AAA*" };
            _mockReadRepository.Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<Student, bool>>>()))
                         .Returns(new List<Student>().AsQueryable());
            _mockWriteRepository.Setup(repo => repo.AddAsync(It.IsAny<Student>())).Returns(Task.FromResult(new Student()));
            _mockWriteRepository.Setup(repo => repo.AddAsync(It.IsAny<Student>()))
                .Returns(Task.FromResult(new Student()));

            // Act
            var result = await _service.CreateStudent(command);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Aluno registrado com sucesso", result.Message);
        }


        [Fact]
        public async Task InactiveStudent_ReturnsFailure_WhenStudentDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockReadRepository.Setup(repo => repo.FindByCondition(x => x.Id == id))
                         .Returns(new List<Student>().AsQueryable());

            // Act
            var result = await _service.InactiveStudent(id);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Falha ao recuperar aluno", result.Message);
        }
    }
}