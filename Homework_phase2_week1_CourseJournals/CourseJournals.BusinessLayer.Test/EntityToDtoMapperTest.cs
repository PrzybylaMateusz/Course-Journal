using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.Mappers;
using CourseJournals.DataLayer.Models;

namespace CourseJournals.BusinessLayer.Test
{
    [TestClass]
    public class EntityToDtoMapperTest
    {
        [TestMethod]
        public void StudentMapping_ProvideValidStudent_ReceiveProperlyMappedStudentDto()
        {
            var studentToMap = new Student();
            var expectedResult = new StudentDto();

            studentToMap.Pesel = 92051692822;
            studentToMap.Name = "Mateusz";
            studentToMap.Surname = "Przybyla";
            studentToMap.BirthDate = new DateTime(05 / 16 / 1992);
            studentToMap.Sex = "Male";

            expectedResult.Pesel = 92051692822;
            expectedResult.Name = "Mateusz";
            expectedResult.Surname = "Przybyla";
            expectedResult.BirthDate = new DateTime(05 / 16 / 1992);
            expectedResult.Sex = "Male";

            var studentMapped = EntityToDtoMapper.StudentEntityModelToDto(studentToMap);
            Assert.AreEqual(expectedResult.Pesel, studentMapped.Pesel);
            Assert.AreEqual(expectedResult.Name, studentMapped.Name);
            Assert.AreEqual(expectedResult.Surname, studentMapped.Surname);
            Assert.AreEqual(expectedResult.BirthDate, studentMapped.BirthDate);
            Assert.AreEqual(expectedResult.Sex, studentMapped.Sex);
        }

        [TestMethod]
        public void StudentListMapping_ProvideValidStudentList_ReceiveProperlyMappedStudentListDto()
        {
            var studentListToMap = new List<Student>();
            var expectedResult = new List<StudentDto>();
            var student1 = new Student();
            var student2 = new Student();


            student1.Pesel = 92051692822;
            student1.Name = "Mateusz";
            student1.Surname = "Przybyla";
            student1.BirthDate = new DateTime(05 / 16 / 1992);
            student1.Sex = "Male";

            student2.Pesel = 92051692821;
            student2.Name = "Mat";
            student2.Surname = "Przyb";
            student2.BirthDate = new DateTime(05 / 18 / 1992);
            student2.Sex = "Male";

            var studentDto1 = EntityToDtoMapper.StudentEntityModelToDto(student1);
            var studentDto2 = EntityToDtoMapper.StudentEntityModelToDto(student2);

            studentListToMap.Add(student1);
            studentListToMap.Add(student2);
            expectedResult.Add(studentDto1);
            expectedResult.Add(studentDto2);

            List<StudentDto> studentListMapped = EntityToDtoMapper.ListOfStudentToListOfStudentDto(studentListToMap);
            CollectionAssert.Equals(expectedResult, studentListMapped);

        }

        [TestMethod]
        public void CourseMapping_ProvideValidCourse_ReceiveProperlyMappedCourseDto()
        {
            var courseToMap = new Course();
            var expectedResult = new CourseDto();

            courseToMap.Id = 125;
            courseToMap.CourseName = "Kurs";
            courseToMap.InstructorName = "Krzysztof";
            courseToMap.InstructorSurname = "Krawczyk";
            courseToMap.StartDate = new DateTime(05 / 05 / 2013);
            courseToMap.MinimalTresholdAttendance = 99;
            courseToMap.MinimalTresholdHomework = 99;
            courseToMap.NumbersOfStudents = 10;

            expectedResult.Id = 125;
            expectedResult.CourseName = "Kurs";
            expectedResult.InstructorName = "Krzysztof";
            expectedResult.InstructorSurname = "Krawczyk";
            expectedResult.StartDate = new DateTime(05 / 05 / 2013);
            expectedResult.MinimalTresholdAttendance = 99;
            expectedResult.MinimalTresholdHomework = 99;
            expectedResult.NumbersOfStudents = 10;

            var courseMapped = EntityToDtoMapper.CourseEntityModelToDto(courseToMap);
            Assert.AreEqual(expectedResult.Id, courseMapped.Id);
            Assert.AreEqual(expectedResult.CourseName, courseMapped.CourseName);
            Assert.AreEqual(expectedResult.InstructorName, courseMapped.InstructorName);
            Assert.AreEqual(expectedResult.InstructorSurname, courseMapped.InstructorSurname);
            Assert.AreEqual(expectedResult.StartDate, courseMapped.StartDate);
            Assert.AreEqual(expectedResult.MinimalTresholdAttendance, courseMapped.MinimalTresholdAttendance);
            Assert.AreEqual(expectedResult.MinimalTresholdHomework, courseMapped.MinimalTresholdHomework);
            Assert.AreEqual(expectedResult.NumbersOfStudents, courseMapped.NumbersOfStudents);
        }
    }
}
