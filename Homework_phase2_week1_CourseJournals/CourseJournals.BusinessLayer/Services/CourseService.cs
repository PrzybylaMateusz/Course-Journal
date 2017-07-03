using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.IServices;
using CourseJournals.BusinessLayer.Mappers;
using CourseJournals.DataLayer.Interfaces;
using CourseJournals.DataLayer.Repositories;
using Newtonsoft.Json;

namespace CourseJournals.BusinessLayer.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICoursesRepository _courseRepository;
        private readonly IStudentService _studentService;
        private readonly IAttendanceService _attendanceService;
        private readonly IHomeworkService _homeworkService;

        public CourseService(ICoursesRepository coursesRepository, IStudentService studentService,
            IAttendanceService attendanceService, IHomeworkService homeworkService)
        {
            _courseRepository = coursesRepository;
            _studentService = studentService;
            _attendanceService = attendanceService;
            _homeworkService = homeworkService;
        }

        public void SaveReportDataToFile(ReportDto report)
        {
            var filepath = Environment.CurrentDirectory + "report.json";
            File.WriteAllText(filepath, JsonConvert.SerializeObject(report));
        }

        public bool AddCourse(CourseDto courseDto)
        {
            if (CheckIfCourseIsInTheDatabaseById(courseDto.Id))
            {
                throw new Exception("The course is already in the database!");
            }
            var course = DtoToEntityMapper.CourseDtoEntityModel(courseDto);
            return _courseRepository.AddCourse(course);
        }

        public bool CheckIfCourseIsInTheDatabaseById(int id)
        {
            var courses = _courseRepository.GetCoursesById(id);
            return courses != null && courses.Count != 0;
        }

        public Dictionary<int, string> GetAllCoursesNames()
        {
            var allCourses = _courseRepository.GetAllCourses();
            return allCourses.ToDictionary(course => course.Id, course => course.CourseName);
        }

        public CourseDto GetCourseDataById(string courseId)
        {
            var course = _courseRepository.GetCoursesDataById(int.Parse(courseId));
            return EntityToDtoMapper.CourseEntityModelToDto(course);
        }

        public int GetValidNumbersOfPoints(string numberOfPoints)
        {
            var number = 0;
            var exit = false;
            while (!exit)
            {
                while (!int.TryParse(numberOfPoints, out number))
                {
                    Console.WriteLine("Value must be a number - try again!");
                    numberOfPoints = Console.ReadLine();
                }
                if (number < 0 || number > 100)
                {
                    Console.WriteLine("The value should be between 0 and 100 - try again. Get number: ");
                    numberOfPoints = Console.ReadLine();
                }
                else
                {
                    exit = true;
                }
            }
            return number;
        }

        public void ChangeCourseData(CourseDto courseDto, string id)
        {
            _courseRepository.ChangeCourseData(DtoToEntityMapper.CourseDtoEntityModel(courseDto), int.Parse(id));
        }

        public void RemoveStudentFromCourse(string id, long pesel)
        {
            _courseRepository.RemoveStudentFromCourse(int.Parse(id), pesel);
        }

        public ReportDto GetReportInfo(string courseId)
        {
            var report = new ReportDto { CourseInfo = GetCourseDataById(courseId) };
            var attendanceList = new List<AttendanceResultDto>();
            var listOfAttendance = _attendanceService.GetAttendanceList(courseId);
            var studentFromCourse = _studentService.GetStudentsList(courseId);
            var maxPoints = _attendanceService.CountDaysNumber(listOfAttendance);

            foreach (var student in studentFromCourse)
            {
                var attendance = new AttendanceResultDto
                {
                    StudentsInfo = student.Name + " " + student.Surname + " " + student.Pesel
                };
                var listOfPresent = _attendanceService.GetMeListOfPresents(student.Pesel);
                attendance.AttendancePoints =
                    _attendanceService.AttendancePoints(student.Pesel, listOfPresent, listOfAttendance);
                attendance.MaxAttendancePoints = maxPoints;
                attendance.AttendancePercents =
                    _attendanceService.CalculateProcenteAttendance(attendance.MaxAttendancePoints,
                        attendance.AttendancePoints);
                attendance.Results = attendance.AttendancePercents >
                    report.CourseInfo.MinimalTresholdAttendance ? "Passed" : "Failed";
                attendanceList.Add(attendance);
            }
            report.AttendanceResults = attendanceList;

            var homewrokList = new List<HomeworkResultDto>();
            var listOfHomeworks = _homeworkService.GetListOfHomework(courseId);
            var maxHomeworkPoints = _homeworkService.MaxPoints(listOfHomeworks);
            foreach (var student in studentFromCourse)
            {
                var homework = new HomeworkResultDto
                {
                    StudentsInfo = student.Name + " " + student.Surname + " " + student.Pesel
                };
                var listOfHomeworkMarks = _homeworkService.GetListOfHomeworkMarks(student.Pesel);
                homework.HomeworkPoints =
                    _homeworkService.CalculateStudentHomeworkPoints(listOfHomeworks, student.Pesel,
                        listOfHomeworkMarks);
                homework.MaxHomeworkPoints = maxHomeworkPoints;
                homework.HomeworkPercents = homework.HomeworkPoints / homework.MaxHomeworkPoints * 100;
                homework.Results = homework.HomeworkPercents >
                    report.CourseInfo.MinimalTresholdHomework ? "Passed" : "Failed";
                homewrokList.Add(homework);
            }
            report.HomeworkResults = homewrokList;

            return report;
        }
    }
}