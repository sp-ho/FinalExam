namespace FinalExam.Models
{
    public class StudentProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
