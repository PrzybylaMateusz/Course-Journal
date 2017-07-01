using System.Collections.Generic;
using CourseJournals.BusinessLayer.Dtos;

namespace CourseJournals.BusinessLayer.Services
{
    public interface ICourseService
    {
        bool AddCourse(CourseDto courseDto);
        bool CheckIfCourseIsInTheDatabaseById(int id);
        Dictionary<int, string> GetAllCoursesNames();
        CourseDto GetCourseDataById(string courseId);
        int GetValidNumbersOfPoints(string numberOfPoints);
        void ChangeCourseData(CourseDto courseDto, string id);
        void RemoveStudentFromCourse(string id, long pesel);
    }
}