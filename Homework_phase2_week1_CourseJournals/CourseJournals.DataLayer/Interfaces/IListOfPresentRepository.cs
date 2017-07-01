using System;
using CourseJournals.DataLayer.Models;

namespace CourseJournals.DataLayer.Repositories
{
    public interface IListOfPresentRepository
    {
        Attendance GetAttendanceDataByIdDate(int courseId, DateTime date);
        bool AddListOfPresent(ListOfPresent listOfPresent);
    }
}