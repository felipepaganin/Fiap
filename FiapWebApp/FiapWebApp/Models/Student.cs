namespace FiapWebApp.Models
{
    public class Student
    {
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; } 
        public string Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
