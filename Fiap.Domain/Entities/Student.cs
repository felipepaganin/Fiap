using Fiap.Domain.Commands.Student;
using Fiap.Shared.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Fiap.Domain.Entities
{
    public class Student : Entity
    {
        public Student(string name, string user, string password)
        {
            Name = name;
            User = user;
            Password = CalculateHash(password);
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

        private string CalculateHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}