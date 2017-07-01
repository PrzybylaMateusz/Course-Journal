namespace CourseJournals.DataLayer.Models
{
    public class Homework
    {
        public int Id { get; set; }
        public string HomeworkName { get; set; }
        public double MaxPoints { get; set; }
        public Course Course { get; set; }
    }
}
