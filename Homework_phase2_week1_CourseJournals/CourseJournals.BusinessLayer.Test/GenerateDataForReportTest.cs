using System;
using System.Collections.Generic;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.Services;
using CourseJournals.DataLayer.Interfaces;
using CourseJournals.DataLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CourseJournals.BusinessLayer.Test
{

    [TestClass]
    public class GenerateDataForReportTest
    {
        [TestMethod]
        public void CheckStudentListIsProperlyGenereted_GetStudentList_ReceiveProperlyStudentList()
        {
            var courseStudentsDto = new List<StudentDto>();
            var courseStudents = new List<Student>();

            var student1 = new Student
            {
                Pesel = 1,
                BirthDate = new DateTime(11 / 11 / 1992),
                Name = "Mateusz",
                Surname = "Przyb",
                Sex = "Male"
            };

            var student2 = new Student()
            {
                Pesel = 2,
                BirthDate = new DateTime(11 / 12 / 1992),
                Name = "Andrzej",
                Surname = "Zaucha",
                Sex = "Male"
            };
            courseStudents.Add(student1);
            courseStudents.Add(student2);

            var studentRepositoryMock = new Mock<IStudentsRepository>();
            studentRepositoryMock.Setup(x => x.GetStudents(111)).Returns(courseStudents);

            var student1Dto = new StudentDto()
            {
                Pesel = 1,
                BirthDate = new DateTime(11 / 11 / 1992),
                Name = "Mateusz",
                Surname = "Przyb",
                Sex = "Male"
            };
            var student2Dto = new StudentDto()
            {
                Pesel = 2,
                BirthDate = new DateTime(11 / 12 / 1992),
                Name = "Andrzej",
                Surname = "Zaucha",
                Sex = "Male"
            };
            courseStudentsDto.Add(student1Dto);
            courseStudentsDto.Add(student2Dto);

            const string courseId = "111";

            var studentService = new StudentService(studentRepositoryMock.Object);
            var result = studentService.GetStudentsList(courseId);
            Equals(courseStudentsDto, result);
        }

        [TestMethod]
        public void DaysNumberCounting_ProvideListOfAttendance_ReceiveProperlyCountedNumberOfDays()
        {
            var attendance1 = new AttendanceDto { Id = 1 };

            var attendance2 = new AttendanceDto { Id = 2 };

            var listOfAttendance = new List<AttendanceDto> { attendance1, attendance2 };

            var attendanceService = new AttendanceService();
            var countedNumberOfDays = attendanceService.CountDaysNumber(listOfAttendance);

            const double expectedResult = 2;

            Assert.AreEqual(expectedResult, countedNumberOfDays);
        }

        [TestMethod]
        public void
            CountingPercentagesOfAttendance_ProvideDaysNUmberAndAttendancePoints_ReceiveProperlyCountedProcenteAttendance()
        {
            var attendanceService = new AttendanceService();
            const int daysNumber = 20;
            const int numberOfPoints = 15;


            var result = attendanceService.CalculateProcenteAttendance(daysNumber, numberOfPoints);
            const double expectedResult = 75;

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void AttendancePoints_ProvideListOfPresenceandAttendanceList_ReceiveNumberOfAttendancePoints()
        {
            var present1 = new ListOfPresentDto();
            var present2 = new ListOfPresentDto();
            var present3 = new ListOfPresentDto();

            present1.Present = "Present";
            present1.Attendance = new AttendanceDto { Id = 1 };
            present1.Student = new StudentDto { Pesel = 111 };

            present2.Present = "Absent";
            present2.Attendance = new AttendanceDto { Id = 2 };
            present2.Student = new StudentDto { Pesel = 111 };

            present3.Present = "Present";
            present3.Attendance = new AttendanceDto { Id = 3 };
            present3.Student = new StudentDto { Pesel = 111 };

            var attendance1 = new AttendanceDto { Id = 1 };
            var attendance2 = new AttendanceDto { Id = 2 };
            var attendance3 = new AttendanceDto { Id = 3 };

            var listOfPresent = new List<ListOfPresentDto> { present1, present2, present3 };
            var listOfAttendaces = new List<AttendanceDto> { attendance1, attendance2, attendance3 };

            const long pesel = 111;
            var attendanceService = new AttendanceService();

            const int expectedResult = 2;
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
            present1.Attendance = new Attendance { Id = 1 };
            present1.Student = new Student { Pesel = 111 };

            present2.Present = "Absent";
            present2.Attendance = new Attendance { Id = 2 };
            present2.Student = new Student { Pesel = 111 };

            present3.Present = "Present";
            present3.Attendance = new Attendance { Id = 3 };
            present3.Student = new Student { Pesel = 111 };

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
            present1Dto.Attendance = new AttendanceDto { Id = 1 };
            present1Dto.Student = new StudentDto { Pesel = 111 };

            present2Dto.Present = "Absent";
            present2Dto.Attendance = new AttendanceDto { Id = 2 };
            present2Dto.Student = new StudentDto { Pesel = 111 };

            present3Dto.Present = "Present";
            present3Dto.Attendance = new AttendanceDto { Id = 3 };
            present3Dto.Student = new StudentDto { Pesel = 111 };

            expectedListOfPresent.Add(present1Dto);
            expectedListOfPresent.Add(present2Dto);
            expectedListOfPresent.Add(present3Dto);

            var attendanceService = new AttendanceService(attendanceRepositoryMock.Object);
            var result = attendanceService.GetMeListOfPresents(111);

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
            homework1.Course = new Course { Id = 111 };

            homework2.Id = 2;
            homework2.HomeworkName = "kurs2";
            homework2.MaxPoints = 100;
            homework2.Course = new Course { Id = 111 };

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
            homework1Dto.Course = new CourseDto { Id = 111 };

            homework2Dto.Id = 2;
            homework2Dto.HomeworkName = "kurs2";
            homework2Dto.MaxPoints = 100;
            homework2Dto.Course = new CourseDto { Id = 111 };

            expectedListOfHomework.Add(homework1Dto);
            expectedListOfHomework.Add(homework2Dto);

            var homeworkService = new HomeworkService(homeworkRepositoryMock.Object);
            var result = homeworkService.GetListOfHomework("111");

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
            homework1.Course = new CourseDto { Id = 111 };

            homework2.Id = 2;
            homework2.HomeworkName = "kurs2";
            homework2.MaxPoints = 100;
            homework2.Course = new CourseDto { Id = 111 };

            listOfHomework.Add(homework1);
            listOfHomework.Add(homework2);

            var homeworkService = new HomeworkService();
            var countedAllPoints = homeworkService.MaxPoints(listOfHomework);
            const int expectedResult = 110;

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
            homeworkMarks1.Student = new Student { Pesel = 111 };
            homeworkMarks1.Homework = new Homework();

            homeworkMarks2.Id = 2;
            homeworkMarks2.HomeworkPoints = 200;
            homeworkMarks2.Student = new Student { Pesel = 111 };
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
            expectedHomeworkMarks1.Student = new StudentDto()
            {
                Pesel = 111
            };
            expectedHomeworkMarks1.HomeworkDto = new HomeworkDto();

            expectedHomeworkMarks2.Id = 2;
            expectedHomeworkMarks2.HomeworkPoints = 200;
            expectedHomeworkMarks2.Student = new StudentDto()
            {
                Pesel = 111
            };
            expectedHomeworkMarks2.HomeworkDto = new HomeworkDto();

            expectedListOfHomeworkMarks.Add(expectedHomeworkMarks1);
            expectedListOfHomeworkMarks.Add(expectedHomeworkMarks2);

            var homeworkService = new HomeworkService(homeworkRepositoryMock.Object);
            var result = homeworkService.GetListOfHomeworkMarks(111);

            Equals(expectedListOfHomeworkMarks, result);
        }

        [TestMethod]
        public void
            CountingStudentHomeworkPoints_ProvideHomeworkInformations_ReceiveProperlyCountedStudentHomeworkPoints()
        {
            var listOfHomeworks = new List<HomeworkDto>();
            var listOfHomeworkMarks = new List<HomeworkMarksDto>();
            const int pesel = 111;

            var homework1 = new HomeworkDto()
            {
                Id = 1,
                MaxPoints = 100
            };
            var homework2 = new HomeworkDto()
            {
                Id = 2,
                MaxPoints = 200
            };
            listOfHomeworks.Add(homework1);
            listOfHomeworks.Add(homework2);

            var homeworkMarks1 = new HomeworkMarksDto()
            {
                HomeworkDto = new HomeworkDto()
                {
                    Id = 1
                },
                Student = new StudentDto()
                {
                    Pesel = 111
                },
                HomeworkPoints = 50
            };
            var homeworkMarks2 = new HomeworkMarksDto()
            {
                HomeworkDto = new HomeworkDto()
                {
                    Id = 2
                },
                Student = new StudentDto()
                {
                    Pesel = 111
                },
                HomeworkPoints = 20
            };
            listOfHomeworkMarks.Add(homeworkMarks1);
            listOfHomeworkMarks.Add(homeworkMarks2);

            var homeworkService = new HomeworkService();
            const int expectedResult = 70;
            var result = homeworkService.CalculateStudentHomeworkPoints(listOfHomeworks, pesel, listOfHomeworkMarks);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
