using System.Collections.Generic;
using System.Linq;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.DataLayer.Models;

namespace CourseJournals.BusinessLayer.Mappers
{
    public class DtoToEntityMapper
    {
        public static Student StudentDtoEntityModel(StudentDto studentDto)
        {
            if (studentDto == null)
            {
                return null;
            }
            var student = new Student
            {
                Pesel = studentDto.Pesel,
                Name = studentDto.Name,
                Surname = studentDto.Surname,
                BirthDate = studentDto.BirthDate,
                Sex = studentDto.Sex
            };
            return student;
        }

        public static Course CourseDtoEntityModel(CourseDto courseDto)
        {
            if (courseDto == null)
            {
                return null;
            }
            var course = new Course
            {
                Id = courseDto.Id,
                CourseName = courseDto.CourseName,
                InstructorName = courseDto.InstructorName,
                InstructorSurname = courseDto.InstructorSurname,
                StartDate = courseDto.StartDate,
                MinimalTresholdAttendance = courseDto.MinimalTresholdAttendance,
                MinimalTresholdHomework = courseDto.MinimalTresholdHomework,
                NumbersOfStudents = courseDto.NumbersOfStudents,
                Students = ListOfStudentDtotoListOfStudent(courseDto.Students)
            };
            return course;
        }

        public static List<Student> ListOfStudentDtotoListOfStudent(List<StudentDto> listStudentDto)
        {
            return listStudentDto?.Select(StudentDtoEntityModel).ToList();
        }

        public static Attendance AttendanceDtoToEntityModel(AttendanceDto attendanceDto)
        {
            if (attendanceDto == null)
            {
                return null;
            }
            var attendance = new Attendance
            {
                Id = attendanceDto.Id,
                DayOfClass = attendanceDto.DayOfClass,
                Courses = CourseDtoEntityModel(attendanceDto.Courses)
            };
            return attendance;
        }

        public static ListOfPresent PresentDtoToPresent(ListOfPresentDto presentDto)
        {
            if (presentDto == null)
            {
                return null;
            }
            var present = new ListOfPresent
            {
                Attendance = AttendanceDtoToEntityModel(presentDto.Attendance),
                Student = StudentDtoEntityModel(presentDto.Student),
                Present = presentDto.Present
            };
            return present;
        }

        public static List<Course> ListOfCoursesDtotoListOfCourses(List<CourseDto> listOfCoursesDto)
        {
            return listOfCoursesDto.Select(CourseDtoEntityModel).ToList();
        }

        public static Homework HomeworkDtotoHomework(HomeworkDto homeworkDto)
        {
            if (homeworkDto == null)
            {
                return null;
            }
            var homework = new Homework
            {
                Id = homeworkDto.Id,
                Course = CourseDtoEntityModel(homeworkDto.Course),
                HomeworkName = homeworkDto.HomeworkName,
                MaxPoints = homeworkDto.MaxPoints,
            };
            return homework;
        }

        public static HomeworkMarks HomeworkMarksDtotoHomeworkMarks(HomeworkMarksDto homeworkMarksDto)
        {
            if (homeworkMarksDto == null)
            {
                return null;
            }
            var homeworkMarks = new HomeworkMarks
            {
                Id = homeworkMarksDto.Id,
                HomeworkPoints = homeworkMarksDto.HomeworkPoints,
                Student = StudentDtoEntityModel(homeworkMarksDto.Student),
                Homework = HomeworkDtotoHomework(homeworkMarksDto.HomeworkDto)
            };
            return homeworkMarks;
        }
    }
}
