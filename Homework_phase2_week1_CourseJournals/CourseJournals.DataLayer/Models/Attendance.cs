using System;

namespace CourseJournals.DataLayer.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime DayOfClass { get; set; }
        public Course Courses { get; set; }
    }
}
