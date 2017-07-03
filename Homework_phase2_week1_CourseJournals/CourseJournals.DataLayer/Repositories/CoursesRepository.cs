using System.Collections.Generic;
using System.Linq;
using CourseJournals.DataLayer.DbContexts;
using CourseJournals.DataLayer.Models;
using System.Data.Entity;
using CourseJournals.DataLayer.Interfaces;

namespace CourseJournals.DataLayer.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        public bool AddCourse(Course course)
        {
            Course addedCourse;
            using (var dbContext = new CourseJournalsDbContext())
            {
                course.Students = course.Students.Select(student => dbContext.StudentsDbSet.Single(s => s.Pesel == student.Pesel)).ToList();

                addedCourse = dbContext.CoursesDbSet.Add(course);
                dbContext.SaveChanges();
            }
            return addedCourse != null;
        }

        public List<Course> GetCoursesById(int id)
        {
            List<Course> courses;
            using (var dbContext = new CourseJournalsDbContext())
            {
                courses = dbContext.CoursesDbSet.Where(c => c.Id == id).ToList();
            }
            return courses;
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses;
            using (var dbContext = new CourseJournalsDbContext())
            {
                courses = dbContext.CoursesDbSet.ToList();
            }
            return courses;
        }

        public Course GetCoursesDataById(int id)
        {
            Course courses;
            using (var dbContext = new CourseJournalsDbContext())
            {
                var allCourses = dbContext.CoursesDbSet.Include(c => c.Students).ToList();
                courses = allCourses.First(a => a.Id == id);
            }
            return courses;
        }

        public void ChangeCourseData(Course course, int id)
        {
            using (var dbContext = new CourseJournalsDbContext())
            {
                dbContext.CoursesDbSet.Attach(course);
                var specyficCourse = dbContext.CoursesDbSet.First(c => c.Id == id);
                specyficCourse.CourseName = course.CourseName;
                specyficCourse.InstructorName = course.InstructorName;
                specyficCourse.InstructorSurname = course.InstructorSurname;
                specyficCourse.MinimalTresholdAttendance = course.MinimalTresholdAttendance;
                specyficCourse.MinimalTresholdHomework = course.MinimalTresholdHomework;
                specyficCourse.NumbersOfStudents = course.NumbersOfStudents;
                dbContext.SaveChanges();
            }
        }

        public void RemoveStudentFromCourse(int id, long pesel)
        {
            using (var dbContext = new CourseJournalsDbContext())
            {
                var specyficCourse = dbContext.CoursesDbSet.Include(a=>a.Students).First(c => c.Id == id);
                var student = specyficCourse.Students.First(s => s.Pesel == pesel);
                specyficCourse.Students.Remove(student);
                dbContext.SaveChanges();
            }
        }
    }
}
