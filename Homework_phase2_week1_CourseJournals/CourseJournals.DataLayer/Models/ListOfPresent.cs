namespace CourseJournals.DataLayer.Models
{
    public class ListOfPresent
    {
        public int Id { get; set; }
        public Attendance Attendance { get; set; }
        public Student Student { get; set; }
        public string Present { get; set; }
    }
}
