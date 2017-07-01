using System.Collections.Generic;
using CourseJournals.BusinessLayer.Dtos;

namespace CourseJournals.BusinessLayer.Services
{
    public interface IHomeworkService
    {
        bool CheckIfTheHomeworkExists(string name);
        bool AddHomework(HomeworkDto homeworkDto);
        List<HomeworkDto> GetListOfHomework(string courseId);
        double MaxPoints(List<HomeworkDto> listOfHomeworks);
        double CalculateStudentHomeworkPoints(List<HomeworkDto> listOfHomeworks, long pesel, List<HomeworkMarksDto> list);
        bool AddHomeworkMarks(HomeworkMarksDto homeworkMarksDto);
        HomeworkDto GetHomeworkByName(string name, string id);
        List<HomeworkMarksDto> GetListOfHomeworkMarks(long pesel);
    }
}