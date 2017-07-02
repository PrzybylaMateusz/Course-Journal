using System;
using System.Collections.Generic;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.Services;
using CourseJournals.DataLayer;
using CourseJournals.DataLayer.Models;
using CourseJournals.DataLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CourseJournals.BusinessLayer.Test
{

    [TestClass]
    public class GenerateDataForReportTest
    {

        [TestMethod]
        public void CheckCourseDataIsProperlyGenerated_GetCourseData_ReceiveProperlyCourseData()
        {
            var course = new Course();
            course.Id = 121;
            course.CourseName = "CodementorsCourse";
            course.InstructorName = "Jan";
            course.InstructorSurname = "Kowalski";
            course.MinimalTresholdAttendance = 80;
            course.MinimalTresholdHomework = 80;
            course.NumbersOfStudents = 2;
            course.StartDate = new DateTime(05 / 05 / 2017);

            var courseRepositoryMock = new Mock<ICoursesRepository>();
            courseRepositoryMock.Setup(x => x.GetCoursesDataById(121)).Returns(course);

            var expectedCourseDto = new CourseDto();
            expectedCourseDto.Id = 121;
            expectedCourseDto.CourseName = "CodementorsCourse";
            expectedCourseDto.InstructorName = "Jan";
            expectedCourseDto.InstructorSurname = "Kowalski";
            expectedCourseDto.MinimalTresholdAttendance = 80;
            expectedCourseDto.MinimalTresholdHomework = 80;
            expectedCourseDto.NumbersOfStudents = 2;
            expectedCourseDto.StartDate = new DateTime(05 / 05 / 2017);

            string courseId = "121";

            var courseService = new CourseService(courseRepositoryMock.Object);
            var result = courseService.GetCourseDataById(courseId);

            Assert.AreEqual(expectedCourseDto.Id, result.Id);
            Assert.AreEqual(expectedCourseDto.CourseName, result.CourseName);
            Assert.AreEqual(expectedCourseDto.InstructorName, result.InstructorName);
            Assert.AreEqual(expectedCourseDto.InstructorSurname, result.InstructorSurname);
            Assert.AreEqual(expectedCourseDto.MinimalTresholdAttendance, result.MinimalTresholdAttendance);
            Assert.AreEqual(expectedCourseDto.MinimalTresholdHomework, result.MinimalTresholdHomework);
            Assert.AreEqual(expectedCourseDto.NumbersOfStudents, result.NumbersOfStudents);
            Assert.AreEqual(expectedCourseDto.StartDate, result.StartDate);
        }

        [TestMethod]
        public void CheckStudentListIsProperlyGenereted_GetStudentList_ReceiveProperlyStudentList()
        {
            var courseStudentsDto = new List<StudentDto>();
            var courseStudents = new List<Student>();

            var student1 = new Student();
            student1.Pesel = 1;
            student1.BirthDate = new DateTime(11 / 11 / 1992);
            student1.Name = "Mateusz";
            student1.Surname = "Przyb";
            student1.Sex = "Male";

            var student2 = new Student();
            student2.Pesel = 2;
            student2.BirthDate = new DateTime(11 / 12 / 1992);
            student2.Name = "Andrzej";
            student2.Surname = "Zaucha";
            student2.Sex = "Male";

            courseStudents.Add(student1);
            courseStudents.Add(student2);

            var studentRepositoryMock = new Mock<IStudentsRepository>();
            studentRepositoryMock.Setup(x => x.GetStudents(111)).Returns(courseStudents);

            var student1Dto = new StudentDto();
            student1Dto.Pesel = 1;
            student1Dto.BirthDate = new DateTime(11 / 11 / 1992);
            student1Dto.Name = "Mateusz";
            student1Dto.Surname = "Przyb";
            student1Dto.Sex = "Male";

            var student2Dto = new StudentDto();
            student2Dto.Pesel = 2;
            student2Dto.BirthDate = new DateTime(11 / 12 / 1992);
            student2Dto.Name = "Andrzej";
            student2Dto.Surname = "Zaucha";
            student2Dto.Sex = "Male";

            courseStudentsDto.Add(student1Dto);
            courseStudentsDto.Add(student2Dto);

            string courseId = "111";

            var studentService = new StudentService(studentRepositoryMock.Object);
            List<StudentDto> result = studentService.GetStudentsList(courseId);
            CollectionAssert.Equals(courseStudentsDto, result);
        }

        [TestMethod]
        public void DaysNumberCounting_ProvideListOfAttendance_ReceiveProperlyCountedNumberOfDays()
        {
            var attendance1 = new AttendanceDto();
            attendance1.Id = 1;

            var attendance2 = new AttendanceDto();
            attendance2.Id = 2;

            List<AttendanceDto> listOfAttendance = new List<AttendanceDto>();
            listOfAttendance.Add(attendance1);
            listOfAttendance.Add(attendance2);

            var attendanceService = new AttendanceService();
            var countedNumberOfDays = attendanceService.CountDaysNumber(listOfAttendance);

            double expectedResult = 2;

            Assert.AreEqual(expectedResult, countedNumberOfDays);
        }

        [TestMethod]
        public void
            CountingPercentagesOfAttendance_ProvideDaysNUmberAndAttendancePoints_ReceiveProperlyCountedProcenteAttendance()
        {
            var attendanceService = new AttendanceService();
            var daysNumber = 20;
            var numberOfPoints = 15;


            var result = attendanceService.CalculateProcenteAttendance(daysNumber, numberOfPoints);
            double expectedResult = 75;

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void AttendancePoints_ProvideListOfPresenceandAttendanceList_ReceiveNumberOfAttendancePoints()
        {
            var present1 = new ListOfPresentDto();
            var present2 = new ListOfPresentDto();
            var present3 = new ListOfPresentDto();

            present1.Present = "Present";
            present1.Attendance = new AttendanceDto();
            present1.Attendance.Id = 1;
            present1.Student = new StudentDto();
            present1.Student.Pesel = 111;

            present2.Present = "Absent";
            present2.Attendance = new AttendanceDto();
            present2.Attendance.Id = 2;
            present2.Student = new StudentDto();
            present2.Student.Pesel = 111;

            present3.Present = "Present";
            present3.Attendance = new AttendanceDto();
            present3.Attendance.Id = 3;
            present3.Student = new StudentDto();
            present3.Student.Pesel = 111;

            var attendance1 = new AttendanceDto();
            attendance1.Id = 1;
            var attendance2 = new AttendanceDto();
            attendance2.Id = 2;
            var attendance3 = new AttendanceDto();
            attendance3.Id = 3;

            List<ListOfPresentDto> listOfPresent = new List<ListOfPresentDto>();
            listOfPresent.Add(present1);
            listOfPresent.Add(present2);
            listOfPresent.Add(present3);
            List<AttendanceDto> listOfAttendaces = new List<AttendanceDto>();
            listOfAttendaces.Add(attendance1);
            listOfAttendaces.Add(attendance2);
            listOfAttendaces.Add(attendance3);

            long pesel = 111;
            var attendanceService = new AttendanceService();

            var expectedResult = 2;
            var result = attendanceService.AttendancePoints(pesel, listOfPresent, listOfAttendaces);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void CheckIfListOfPresentIsProperlyGenereted_GetListOfPresent_ReceiveProperlyListOfPresent()
        {
            var listOfPresent = new List<ListOfPresent>();

            var present1 = new ListOfPresent();
            var present2 = new ListOfPresent();
            var present3 = new ListOfPresent();

            present1.Present = "Present";
            present1.Attendance = new Attendance();
            present1.Attendance.Id = 1;
            present1.Student = new Student();
            present1.Student.Pesel = 111;

            present2.Present = "Absent";
            present2.Attendance = new Attendance();
            present2.Attendance.Id = 2;
            present2.Student = new Student();
            present2.Student.Pesel = 111;

            present3.Present = "Present";
            present3.Attendance = new Attendance();
            present3.Attendance.Id = 3;
            present3.Student = new Student();
            present3.Student.Pesel = 111;

            listOfPresent.Add(present1);
            listOfPresent.Add(present2);
            listOfPresent.Add(present3);

            var attendanceRepositoryMock = new Mock<IAttendanceRepositores>();
            attendanceRepositoryMock.Setup(x => x.GetListOfAllPresentsAtCourse(111)).Returns(listOfPresent);

            var expectedListOfPresent = new List<ListOfPresentDto>();

            var present1Dto = new ListOfPresentDto();
            var present2Dto = new ListOfPresentDto();
            var present3Dto = new ListOfPresentDto();

            present1Dto.Present = "Present";
            present1Dto.Attendance = new AttendanceDto();
            present1Dto.Attendance.Id = 1;
            present1Dto.Student = new StudentDto();
            present1Dto.Student.Pesel = 111;

            present2Dto.Present = "Absent";
            present2Dto.Attendance = new AttendanceDto();
            present2Dto.Attendance.Id = 2;
            present2Dto.Student = new StudentDto();
            present2Dto.Student.Pesel = 111;

            present3Dto.Present = "Present";
            present3Dto.Attendance = new AttendanceDto();
            present3Dto.Attendance.Id = 3;
            present3Dto.Student = new StudentDto();
            present3Dto.Student.Pesel = 111;

            expectedListOfPresent.Add(present1Dto);
            expectedListOfPresent.Add(present2Dto);
            expectedListOfPresent.Add(present3Dto);

            var attendanceService = new AttendanceService(attendanceRepositoryMock.Object);
            List<ListOfPresentDto> result = attendanceService.GetMeListOfPresents(111);

            Equals(expectedListOfPresent, result);
        }

        [TestMethod]
        public void CheckIfListOfHomeworkIsProperlyGenereted_GetListOfHomework_ReceiveProperlyListOfHomework()
        {
            var listOfHomework = new List<Homework>();
            var homework1 = new Homework();
            var homework2 = new Homework();

            homework1.Id = 1;
            homework1.HomeworkName = "kurs1";
            homework1.MaxPoints = 10;
            homework1.Course = new Course();
            homework1.Course.Id = 111;

            homework2.Id = 2;
            homework2.HomeworkName = "kurs2";
            homework2.MaxPoints = 100;
            homework2.Course = new Course();
            homework2.Course.Id = 111;

            listOfHomework.Add(homework1);
            listOfHomework.Add(homework2);

            var homeworkRepositoryMock = new Mock<IHomeworkRepository>();
            homeworkRepositoryMock.Setup(x => x.GetHomeWorkByCourseId(111)).Returns(listOfHomework);

            var expectedListOfHomework = new List<HomeworkDto>();
            var homework1Dto = new HomeworkDto();
            var homework2Dto = new HomeworkDto();

            homework1Dto.Id = 1;
            homework1Dto.HomeworkName = "kurs1";
            homework1Dto.MaxPoints = 10;
            homework1Dto.Course = new CourseDto();
            homework1Dto.Course.Id = 111;

            homework2Dto.Id = 2;
            homework2Dto.HomeworkName = "kurs2";
            homework2Dto.MaxPoints = 100;
            homework2Dto.Course = new CourseDto();
            homework2Dto.Course.Id = 111;

            expectedListOfHomework.Add(homework1Dto);
            expectedListOfHomework.Add(homework2Dto);

            var homeworkService = new HomeworkService(homeworkRepositoryMock.Object);
            List<HomeworkDto> result = homeworkService.GetListOfHomework("111");

            Equals(expectedListOfHomework, result);
        }

        [TestMethod]
        public void AllPointsCounting_ProvideListOfHomework_ReceiveProperlyCountedAllPoints()
        {
            var homework1 = new HomeworkDto();
            var homework2 = new HomeworkDto();
            var listOfHomework = new List<HomeworkDto>();

            homework1.Id = 1;
            homework1.HomeworkName = "kurs1";
            homework1.MaxPoints = 10;
            homework1.Course = new CourseDto();
            homework1.Course.Id = 111;

            homework2.Id = 2;
            homework2.HomeworkName = "kurs2";
            homework2.MaxPoints = 100;
            homework2.Course = new CourseDto();
            homework2.Course.Id = 111;

            listOfHomework.Add(homework1);
            listOfHomework.Add(homework2);

            var homeworkService = new HomeworkService();
            var countedAllPoints = homeworkService.MaxPoints(listOfHomework);
            var expectedResult = 110;

            Assert.AreEqual(expectedResult, countedAllPoints);
        }

        [TestMethod]
        public void CheckIfListOfHomeworkMarksIsProperlyGenerated_GetListOfHomeworkMarks_ReceiveProperlyListOfHomeworkMarks()
        {
            var listOfHomeworkMarks = new List<HomeworkMarks>();
            var homeworkMarks1 = new HomeworkMarks();
            var homeworkMarks2 = new HomeworkMarks();

            homeworkMarks1.Id = 1;
            homeworkMarks1.HomeworkPoints = 100;
            homeworkMarks1.Student = new Student();
            homeworkMarks1.Student.Pesel = 111;
            homeworkMarks1.Homework = new Homework();

            homeworkMarks2.Id = 2;
            homeworkMarks2.HomeworkPoints = 200;
            homeworkMarks2.Student = new Student();
            homeworkMarks2.Student.Pesel = 111;
            homeworkMarks2.Homework = new Homework();

            listOfHomeworkMarks.Add(homeworkMarks1);
            listOfHomeworkMarks.Add(homeworkMarks2);

            var homeworkRepositoryMock = new Mock<IHomeworkRepository>();
            homeworkRepositoryMock.Setup(x => x.GetHomeWorkMarksByPesel(111)).Returns(listOfHomeworkMarks);

            var expectedListOfHomeworkMarks = new List<HomeworkMarksDto>();
            var expectedHomeworkMarks1 = new HomeworkMarksDto();
            var expectedHomeworkMarks2 = new HomeworkMarksDto();

            expectedHomeworkMarks1.Id = 1;
            expectedHomeworkMarks1.HomeworkPoints = 100;
            expectedHomeworkMarks1.Student = new StudentDto();
            expectedHomeworkMarks1.Student.Pesel = 111;
            expectedHomeworkMarks1.HomeworkDto = new HomeworkDto();

            expectedHomeworkMarks2.Id = 2;
            expectedHomeworkMarks2.HomeworkPoints = 200;
            expectedHomeworkMarks2.Student = new StudentDto();
            expectedHomeworkMarks2.Student.Pesel = 111;
            expectedHomeworkMarks2.HomeworkDto = new HomeworkDto();

            expectedListOfHomeworkMarks.Add(expectedHomeworkMarks1);
            expectedListOfHomeworkMarks.Add(expectedHomeworkMarks2);

            var homeworkService = new HomeworkService(homeworkRepositoryMock.Object);
            List<HomeworkMarksDto> result = homeworkService.GetListOfHomeworkMarks(111);

            Equals(expectedListOfHomeworkMarks, result);
        }

        [TestMethod]
        public void
            CountingStudentHomeworkPoints_ProvideHomeworkInformations_ReceiveProperlyCountedStudentHomeworkPoints()
        {
            var listOfHomeworks = new List<HomeworkDto>();
            var listOfHomeworkMarks = new List<HomeworkMarksDto>();
            var pesel = 111;

            var homework1 = new HomeworkDto();
            homework1.Id = 1;
            homework1.MaxPoints = 100;

            var homework2 = new HomeworkDto();
            homework2.Id = 2;
            homework2.MaxPoints = 200;

            listOfHomeworks.Add(homework1);
            listOfHomeworks.Add(homework2);

            var homeworkMarks1 = new HomeworkMarksDto();
            homeworkMarks1.HomeworkDto = new HomeworkDto(); 
            homeworkMarks1.HomeworkDto.Id = 1;
            homeworkMarks1.Student = new StudentDto();
            homeworkMarks1.Student.Pesel = 111;
            homeworkMarks1.HomeworkPoints = 50;

            var homeworkMarks2 = new HomeworkMarksDto();
            homeworkMarks2.HomeworkDto = new HomeworkDto();
            homeworkMarks2.HomeworkDto.Id = 2;
            homeworkMarks2.Student = new StudentDto();
            homeworkMarks2.Student.Pesel = 111;
            homeworkMarks2.HomeworkPoints = 20;

            listOfHomeworkMarks.Add(homeworkMarks1);
            listOfHomeworkMarks.Add(homeworkMarks2);

            var homeworkService = new HomeworkService();
            var expectedResult = 70;
            var result = homeworkService.CalculateStudentHomeworkPoints(listOfHomeworks, pesel, listOfHomeworkMarks);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
