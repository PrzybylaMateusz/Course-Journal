using System;
using System.Collections.Generic;
using System.IO;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.Mappers;
using CourseJournals.DataLayer;
using CourseJournals.DataLayer.Repositories;
using Newtonsoft.Json;

namespace CourseJournals.BusinessLayer.Services
{
    public class CourseService : ICourseService
    {
        private ICoursesRepository _courseRepository;
        private IStudentService _studentService;
        private IAttendanceService _attendanceService;
        private IHomeworkService _homeworkService;

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
            string filepath = Environment.CurrentDirectory + "report.json";
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
            var result = new Dictionary<int, string>();
            var allCourses = _courseRepository.GetAllCourses();
            foreach (var course in allCourses)
            {
                result.Add(course.Id, course.CourseName);
            }
            return result;
        }

        public CourseDto GetCourseDataById(string courseId)
        {
            var course = _courseRepository.GetCoursesDataById(Int32.Parse(courseId));
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
            _courseRepository.ChangeCourseData(DtoToEntityMapper.CourseDtoEntityModel(courseDto), Int32.Parse(id));
        }

        public void RemoveStudentFromCourse(string id, long pesel)
        {
            _courseRepository.RemoveStudentFromCourse(Int32.Parse(id), pesel);
        }

        public ReportDto GetReportInfo(string courseId)
        {
            var report = new ReportDto();
            report.CourseInfo = GetCourseDataById(courseId);
            var attendanceList = new List<AttendanceResultDto>();
            var listOfAttendance = _attendanceService.GetAttendanceList(courseId);
            var studentFromCourse = _studentService.GetStudentsList(courseId);
            var maxPoints = _attendanceService.CountDaysNumber(listOfAttendance);

            foreach (var student in studentFromCourse)
            {
                var attendance = new AttendanceResultDto();
                attendance.StudentsInfo = student.Name + " " + student.Surname + " " + student.Pesel;
                var listOfPresent = _attendanceService.GetMeListOfPresents(student.Pesel);
                attendance.AttendancePoints =
                    _attendanceService.AttendancePoints(student.Pesel, listOfPresent, listOfAttendance);
                attendance.MaxAttendancePoints = maxPoints;
                attendance.AttendancePercents =
                    _attendanceService.CalculateProcenteAttendance(attendance.MaxAttendancePoints,
                        attendance.AttendancePoints);
                if (attendance.AttendancePercents > report.CourseInfo.MinimalTresholdAttendance)
                {
                    attendance.Results = "Passed";
                }
                else
                {
                    attendance.Results = "Failed";
                }
                attendanceList.Add(attendance);
            }
            report.AttendanceResults = attendanceList;

            var homewrokList = new List<HomeworkResultDto>();
            var listOfHomeworks = _homeworkService.GetListOfHomework(courseId);
            var maxHomeworkPoints = _homeworkService.MaxPoints(listOfHomeworks);
            foreach (var student in studentFromCourse)
            {
                var homework = new HomeworkResultDto();
                homework.StudentsInfo = student.Name + " " + student.Surname + " " + student.Pesel;
                var listOfHomeworkMarks = _homeworkService.GetListOfHomeworkMarks(student.Pesel);
                homework.HomeworkPoints =
                    _homeworkService.CalculateStudentHomeworkPoints(listOfHomeworks, student.Pesel,
                        listOfHomeworkMarks);
                homework.MaxHomeworkPoints = maxHomeworkPoints;
                homework.HomeworkPercents = homework.HomeworkPoints / homework.MaxHomeworkPoints * 100;
                if (homework.HomeworkPercents > report.CourseInfo.MinimalTresholdHomework)
                {
                    homework.Results = "Passed";
                }
                else
                {
                    homework.Results = "Failed";
                }
                homewrokList.Add(homework);
            }
            report.HomeworkResults = homewrokList;

            return report;
        }
    }
}