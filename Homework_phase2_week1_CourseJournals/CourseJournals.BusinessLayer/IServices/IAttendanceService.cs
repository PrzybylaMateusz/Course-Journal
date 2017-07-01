using System;
using System.Collections.Generic;
using CourseJournals.BusinessLayer.Dtos;

namespace CourseJournals.BusinessLayer.Services
{
    public interface IAttendanceService
    {
        bool CheckIfDataIsInTheDatabase(DateTime date);
        bool CheckIfPresentIsCorrectValue(string present);
        bool AddAttendance(AttendanceDto attendanceDto);
        List<AttendanceDto> GetAttendanceList(string courseId);
        double CountDaysNumber(List<AttendanceDto> dayList);
        List<ListOfPresentDto> GetMeListOfPresents(long pesel);
        double AttendancePoints(long pesel, List<ListOfPresentDto> list, List<AttendanceDto> attendanceList);
        double CalculateProcenteAttendance(double daysNumber, double numberOfPoints);
        AttendanceDto Attendance(string courseId, DateTime date);
    }
}