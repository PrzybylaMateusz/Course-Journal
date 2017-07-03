using System;
using CourseJournals.DataLayer.Models;

namespace CourseJournals.DataLayer.Interfaces
{
    public interface IListOfPresentRepository
    {
        Attendance GetAttendanceDataByIdDate(int courseId, DateTime date);
        bool AddListOfPresent(ListOfPresent listOfPresent);
    }
}