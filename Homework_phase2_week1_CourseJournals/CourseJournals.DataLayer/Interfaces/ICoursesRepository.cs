using System.Collections.Generic;
using CourseJournals.DataLayer.Models;

namespace CourseJournals.DataLayer.Interfaces
{
    public interface ICoursesRepository
    {
        bool AddCourse(Course course);
        List<Course> GetCoursesById(int id);
        List<Course> GetAllCourses();
        Course GetCoursesDataById(int id);
        void ChangeCourseData(Course course, int id);
        void RemoveStudentFromCourse(int id, long pesel);
    }
}