namespace FinalExam.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
        public string? Grade { get; set; }
        public bool IsCompleted { get; set; }       

    }
}
