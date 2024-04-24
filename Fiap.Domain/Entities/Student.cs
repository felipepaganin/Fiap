using Fiap.Domain.Commands.Student;
using Fiap.Shared.Entities;

namespace Fiap.Domain.Entities
{
    public class Student : Entity
    {
        public Student(string name, string user, string password)
        {
            Name = name;
            User = user;
            Password = password;
        }

        public Student() { }

        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public ICollection<ClassStudent> ClassStudents { get; set; }


        public List<Class> Class { get; } = [];

        public void Update(UpdateStudentCommand command)
        {
            Name = command.Name;
            User = command.User;
            Password = command.Password;
        }

        public void Inactive()
        {
            Active = false;
        }
    }
}