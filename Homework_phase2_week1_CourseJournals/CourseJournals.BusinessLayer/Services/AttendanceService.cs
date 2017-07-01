using System;
using System.Collections.Generic;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.DataLayer.Repositories;
using CourseJournals.BusinessLayer.Mappers;


namespace CourseJournals.BusinessLayer.Services
{
    public class AttendanceService : IAttendanceService
    {
    
        private IAttendanceRepositores _attendanceRepositores;

        public AttendanceService(IAttendanceRepositores attendanceRepositores)
        {
            _attendanceRepositores = attendanceRepositores;
        }

      public bool CheckIfDataIsInTheDatabase(DateTime date)
        {
            var day = _attendanceRepositores.GetDayInSystem(date);

            if (day == null || day.Count == 0)
            {
                return false;
            }

            return true;
        }
        public bool CheckIfPresentIsCorrectValue(string present)
        {
            if (present == "Absent" || present == "Present")
            {
                return true;
            }
            return false;
        }

        public bool AddAttendance(AttendanceDto attendanceDto)
        {
            var attendance = DtoToEntityMapper.AttendanceDtoToEntityModel(attendanceDto);
            return _attendanceRepositores.AddAttendance(attendance);
        }
        public List<AttendanceDto> GetAttendanceList(string courseId)
        {
            var attendanceList = new List<AttendanceDto>();
            var list = _attendanceRepositores.GetAttendanceDays(Int32.Parse(courseId));
            foreach (var days in list)
            {
                AttendanceDto attendance = EntityToDtoMapper.AttendanceEntityModelToDto(days);
                attendanceList.Add(attendance);
            }
            return attendanceList;
        }

        public double CountDaysNumber(List<AttendanceDto> dayList)
        {
            double totalNumber=0;
            foreach (var unused in dayList)
            {
                totalNumber++;
            }
            return totalNumber;
        }

        public List<ListOfPresentDto> GetMeListOfPresents(long pesel)
        {
            var listOfAllPresents = new List<ListOfPresentDto>();
            var list = _attendanceRepositores.GetListOfAllPresentsAtCourse(pesel);
            foreach (var record in list)
            {
                ListOfPresentDto listOfPresent = EntityToDtoMapper.ListOfPresentsEntityModelToDto(record);
                listOfAllPresents.Add(listOfPresent);
            }
            return listOfAllPresents;
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
            double result = (numberOfPoints / daysNumber) * 100;
            return result;
        }

        public AttendanceDto Attendance(string courseId, DateTime date)
        {
            var attendance = new AttendanceDto();
            var list = _attendanceRepositores.GetAttendanceDays(Int32.Parse(courseId));
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
