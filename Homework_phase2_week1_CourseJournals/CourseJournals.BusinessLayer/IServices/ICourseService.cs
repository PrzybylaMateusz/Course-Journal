using System.Collections.Generic;
using CourseJournals.BusinessLayer.Dtos;

namespace CourseJournals.BusinessLayer.IServices
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
        ReportDto GetReportInfo(string courseId);
        void SaveReportDataToFile(ReportDto report);
    }
}