using System.Collections.Generic;
using CourseJournals.DataLayer.Models;

namespace CourseJournals.DataLayer.Interfaces
{
    public interface IHomeworkRepository
    {
        List<Homework> GetHomeWorkByName(string name);
        bool AddHomework(Homework homework);
        List<Homework> GetHomeWorkByCourseId(int id);
        bool AddHomeworkMarks(HomeworkMarks homeworkMarks);
        List<HomeworkMarks> GetHomeWorkMarksByPesel(long pesel);
    }
}