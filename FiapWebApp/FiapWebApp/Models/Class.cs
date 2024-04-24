namespace FiapWebApp.Models
{
    public class Class
    {
        public Class() { }

        public Class(string name, int year)
        {
            Name = name;
            Year = year;
        }

        public string Name { get; set; }
        public int Year { get; set; }
    }
}
