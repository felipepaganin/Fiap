using Fiap.Domain.Commands.Class;
using Fiap.Shared.Entities;

namespace Fiap.Domain.Entities
{
    public class Class : Entity
    {
        public Class(string className, int year)
        {
            ClassName = className.ToUpper();
            Year = year;
        }

        public Class() { }

        public string ClassName { get; set; }
        public int Year { get; set; }
        public List<Student> Students { get; } = [];
        public ICollection<ClassStudent> ClassStudents { get; set; }


        public void Update(UpdateClassCommand command)
        {
            ClassName = command.ClassName.ToUpper();
            Year = command.Year;
        }

        public void Inactive()
        {
            Active = false;
        }

    }
}
