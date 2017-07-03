using System;
using System.Collections.Generic;
using CourseJournals.DataLayer.Models;

namespace CourseJournals.DataLayer.Interfaces
{
    public interface IAttendanceRepositores
    {
        List<Attendance> GetDayInSystem(DateTime date);
        bool AddAttendance(Attendance attendance);
        List<Attendance> GetAllDays();
        List<Attendance> GetAttendanceDays(int id);
        List<ListOfPresent> GetListOfAllPresentsAtCourse(long pesel);
    }
}