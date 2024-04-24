namespace Fiap.Domain.Entities
{
    public class ClassStudent
    {
        public ClassStudent(Guid classId, Guid studentId)
        {
            ClassId = classId;
            StudentId = studentId;
        }

        public ClassStudent() { }
        public Guid ClassId { get; set; }
        public Class Class { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }

    }
}
