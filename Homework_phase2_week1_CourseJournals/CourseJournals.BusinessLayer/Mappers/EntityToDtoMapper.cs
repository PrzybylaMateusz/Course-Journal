using System.Collections.Generic;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.DataLayer.Models;

namespace CourseJournals.BusinessLayer.Mappers
{
   public class EntityToDtoMapper
    {
        public static CourseDto CourseEntityModelToDto(Course course)
        {
            if (course == null)
            {
                return null;
            }
            var courseDto = new CourseDto
            {
                Id = course.Id,
                CourseName = course.CourseName,
                InstructorName = course.InstructorName,
                InstructorSurname = course.InstructorSurname,
                StartDate = course.StartDate,
                MinimalTresholdAttendance = course.MinimalTresholdAttendance,
                MinimalTresholdHomework = course.MinimalTresholdHomework,
                NumbersOfStudents = course.NumbersOfStudents,
                Students = ListOfStudentToListOfStudentDto(course.Students)
            };
            return courseDto;
        }

        public static List<StudentDto> ListOfStudentToListOfStudentDto(List<Student> listStudent)
        {
            if (listStudent == null)
            {
                return null;
            }
            List<StudentDto> list = new List<StudentDto>();
            foreach (var student in listStudent)
            {
                list.Add(StudentEntityModelToDto(student));
            }
            return list;
        }

        public static StudentDto StudentEntityModelToDto(Student student)
        {
            if (student == null)
            {
                return null;
            }
            var studentDto = new StudentDto
            {
                Pesel = student.Pesel,
                Name = student.Name,
                Surname = student.Surname,
                BirthDate = student.BirthDate,
                Sex = student.Sex,
            };
            return studentDto;
        }

        public static List<CourseDto> ListOfCoursesToListOfCoursesDto(List<Course> listCourse)
        {
            if (listCourse == null)
            {
                return null;
            }
            List<CourseDto> list = new List<CourseDto>();
            foreach (var course in listCourse)
            {
                list.Add(CourseEntityModelToDto(course));
            }
            return list;
        }

        public static AttendanceDto AttendanceEntityModelToDto(Attendance attendance)
        {
            if (attendance == null)
            {
                return null;
            }
            var attendanceDto = new AttendanceDto
            {
                Id = attendance.Id,
                DayOfClass = attendance.DayOfClass,
                Courses = CourseEntityModelToDto(attendance.Courses)
            };
            return attendanceDto;
        }

        public static ListOfPresentDto ListOfPresentsEntityModelToDto (ListOfPresent listOfPresent)
        {
            if (listOfPresent == null)
            {
                return null;
            }
            var listOfPresentDto = new ListOfPresentDto
            {
                Id = listOfPresent.Id,
                Student = StudentEntityModelToDto(listOfPresent.Student),
                Present = listOfPresent.Present,
                Attendance = AttendanceEntityModelToDto(listOfPresent.Attendance)
            };
            return listOfPresentDto;
        }

        public static HomeworkDto HomeworkEntityModelToDto(Homework homework)
        {
            if (homework == null)
            {
                return null;
            }
            var homeworkDto = new HomeworkDto
            {
                Id = homework.Id,
                HomeworkName = homework.HomeworkName,
                Course = CourseEntityModelToDto(homework.Course),
                MaxPoints = homework.MaxPoints
            };
            return homeworkDto;
        }

        public static HomeworkMarksDto HomeworkMarksEntityModelToDto(HomeworkMarks homeworkMarks)
        {
            if (homeworkMarks == null)
            {
                return null;
            }
            var homeworkMarksDto = new HomeworkMarksDto
            {
                Student = StudentEntityModelToDto(homeworkMarks.Student),
                HomeworkPoints = homeworkMarks.HomeworkPoints,
                Id = homeworkMarks.Id,
                HomeworkDto = HomeworkEntityModelToDto(homeworkMarks.Homework)
            };
            return homeworkMarksDto;
        }
    }
}
