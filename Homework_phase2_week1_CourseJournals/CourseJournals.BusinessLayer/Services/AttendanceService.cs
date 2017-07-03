using System;
using System.Collections.Generic;
using System.Linq;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.IServices;
using CourseJournals.DataLayer.Repositories;
using CourseJournals.BusinessLayer.Mappers;
using CourseJournals.DataLayer.Interfaces;

namespace CourseJournals.BusinessLayer.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepositores _attendanceRepositores;

        public AttendanceService(IAttendanceRepositores attendanceRepositores)
        {
            _attendanceRepositores = attendanceRepositores;
        }

        public AttendanceService()
        {
        }

        public bool CheckIfDataIsInTheDatabase(DateTime date, string id)
        {
            var day = _attendanceRepositores.GetDayInSystem(date);

            if (day == null || day.Count == 0)
            {
                return false;
            }
            return day.Any(d => d.Courses.Id == int.Parse(id));
        }

        public bool CheckIfPresentIsCorrectValue(string present)
        {
            return present == "Absent" || present == "Present";
        }

        public bool AddAttendance(AttendanceDto attendanceDto)
        {
            var attendance = DtoToEntityMapper.AttendanceDtoToEntityModel(attendanceDto);
            return _attendanceRepositores.AddAttendance(attendance);
        }

        public List<AttendanceDto> GetAttendanceList(string courseId)
        {
            var list = _attendanceRepositores.GetAttendanceDays(Int32.Parse(courseId));
            return list.Select(days => EntityToDtoMapper.AttendanceEntityModelToDto(days)).ToList();
        }

        public double CountDaysNumber(List<AttendanceDto> dayList)
        {
            double totalNumber = 0;
            foreach (var unused in dayList)
            {
                totalNumber++;
            }
            return totalNumber;
        }

        public List<ListOfPresentDto> GetMeListOfPresents(long pesel)
        {
            var list = _attendanceRepositores.GetListOfAllPresentsAtCourse(pesel);
            return list.Select(EntityToDtoMapper.ListOfPresentsEntityModelToDto).ToList();
        }

        public double AttendancePoints(long pesel, List<ListOfPresentDto> list, List<AttendanceDto> attendanceList)
        {
            double sumOfAllStudentPkt = 0;
            foreach (var attendance in attendanceList)
            {
                foreach (var record in list)
                {
                    if (record.Student.Pesel == pesel && record.Present == "Present" && attendance.Id == record.Attendance.Id)
                    {
                        sumOfAllStudentPkt++;
                    }
                }
            }
            return sumOfAllStudentPkt;
        }

        public double CalculateProcenteAttendance(double daysNumber, double numberOfPoints)
        {
            var result = (numberOfPoints / daysNumber) * 100;
            return result;
        }

        public AttendanceDto Attendance(string courseId, DateTime date)
        {
            var attendance = new AttendanceDto();
            var list = _attendanceRepositores.GetAttendanceDays(int.Parse(courseId));
            foreach (var days in list)
            {
                if (days.DayOfClass == date)
                {
                    attendance = EntityToDtoMapper.AttendanceEntityModelToDto(days);
                }
            }
            return attendance;
        }
    }
}
