namespace CourseJournals.DataLayer.Models
{
    public class HomeworkMarks
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public double HomeworkPoints { get; set; }
        public Homework Homework { get; set; }
    }
}
