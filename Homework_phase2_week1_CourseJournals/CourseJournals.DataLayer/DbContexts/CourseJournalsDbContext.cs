using System.Configuration;
using System.Data.Entity;
using CourseJournals.DataLayer.Models;


namespace CourseJournals.DataLayer.DbContexts
{
    internal class CourseJournalsDbContext : DbContext
    {
        public CourseJournalsDbContext() : base(GetConnectionString())
        { }

        public DbSet<Student> StudentsDbSet { get; set; }
        public DbSet<Course> CoursesDbSet { get; set; }
        public DbSet<Attendance> AttendanceDbSet { get; set; }
        public DbSet<Homework> HomeworkDbSet { get; set; }
        public DbSet<ListOfPresent> ListOfPresentDbSet { get; set; }
        public DbSet<HomeworkMarks> HomeworkMarksDbSet { get; set; }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["CourseJournalsDbMateuszPrzybyla"].ConnectionString;
        }
    }
}
