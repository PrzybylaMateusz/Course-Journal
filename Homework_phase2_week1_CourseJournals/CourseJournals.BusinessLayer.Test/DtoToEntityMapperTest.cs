using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.Mappers;
using CourseJournals.DataLayer.Models;
using System.Collections.Generic;

namespace CourseJournals.BusinessLayer.Test
{
   [TestClass]
    public class DtoToEntityMapperTest
    {
        [TestMethod]
        public void StudentDtoMapping_ProvideValidStudentDto_ReceiveProperlyMappedStudent()
        {
            var studentDtoToMap = new StudentDto();
            var expectedResult = new Student();

            studentDtoToMap.Pesel = 92051692822;
            studentDtoToMap.Name = "Mateusz";
            studentDtoToMap.Surname = "Przyb";
            studentDtoToMap.BirthDate = new DateTime(05 / 06 / 1992);
            studentDtoToMap.Sex = "Male";

            expectedResult.Pesel = 92051692822;
            expectedResult.Name = "Mateusz";
            expectedResult.Surname = "Przyb";
            expectedResult.BirthDate = new DateTime(05 / 06 / 1992);
            expectedResult.Sex = "Male";

            var studentDtoToMapNull = new StudentDto();
            studentDtoToMapNull = null;

            var studentMapped = DtoToEntityMapper.StudentDtoEntityModel(studentDtoToMap);

            var studentMappedNull = DtoToEntityMapper.StudentDtoEntityModel(studentDtoToMapNull);
            var expectedResult2 = new Student();
            expectedResult2 = null;

            Assert.AreEqual(expectedResult.Name, studentMapped.Name);
            Assert.AreEqual(expectedResult.Surname, studentMapped.Surname);
            Assert.AreEqual(expectedResult.Pesel, studentMapped.Pesel);
            Assert.AreEqual(expectedResult.BirthDate, studentMapped.BirthDate);
            Assert.AreEqual(expectedResult.Sex, studentMapped.Sex);
            Assert.AreEqual(expectedResult2, studentMappedNull);
        }

        [TestMethod]
        public void StudentDtoListMapping_ProvideValidStudentDtoList_ReceiveProperlyMappedStudentList()
        {
            var studentDtoListToMap = new List<StudentDto>();
            var expectedResult = new List<Student>();
            var studentDto1 = new StudentDto();
            var studentDto2 = new StudentDto();

            studentDto1.Pesel = 92051692822;
            studentDto1.Name = "Mateusz";
            studentDto1.Surname = "Przybyla";
            studentDto1.BirthDate = new DateTime(05 / 16 / 1992);
            studentDto1.Sex = "Male";

            studentDto2.Pesel = 92051692821;
            studentDto2.Name = "Mat";
            studentDto2.Surname = "Przyb";
            studentDto2.BirthDate = new DateTime(05 / 18 / 1992);
            studentDto2.Sex = "Male";

            var student1 = DtoToEntityMapper.StudentDtoEntityModel(studentDto1);
            var student2 = DtoToEntityMapper.StudentDtoEntityModel(studentDto2);

            studentDtoListToMap.Add(studentDto1);
            studentDtoListToMap.Add(studentDto2);
            expectedResult.Add(student1);
            expectedResult.Add(student2);

            List<Student> studentListMapped = DtoToEntityMapper.ListOfStudentDtotoListOfStudent(studentDtoListToMap);
            CollectionAssert.Equals(expectedResult, studentListMapped);
        }

        [TestMethod]
        public void CourseDtoMapping_ProvideValidCourseDto_ReceiveProperlyMappedCourse()
        {
            var courseDtoToMap = new CourseDto();
            var expectedResult = new Course();

            var studentDto1 = new StudentDto();
            var studentDto2 =new StudentDto();

            studentDto1.Pesel = 92051692822;
            studentDto1.Name = "Mateusz";
            studentDto1.Surname = "Przybyla";
            studentDto1.BirthDate = new DateTime(05 / 16 / 1992);
            studentDto1.Sex = "Male";

            studentDto2.Pesel = 92051692821;
            studentDto2.Name = "Mat";
            studentDto2.Surname = "Przyb";
            studentDto2.BirthDate = new DateTime(05 / 18 / 1992);
            studentDto2.Sex = "Male";

            var student1 = DtoToEntityMapper.StudentDtoEntityModel(studentDto1);
            var student2 = DtoToEntityMapper.StudentDtoEntityModel(studentDto2);

            courseDtoToMap.Id = 125;
            courseDtoToMap.CourseName = "Kurs";
            courseDtoToMap.InstructorName = "Krzysztof";
            courseDtoToMap.InstructorSurname = "Krawczyk";
            courseDtoToMap.StartDate = new DateTime(05 / 05 / 2013);
            courseDtoToMap.MinimalTresholdAttendance = 99;
            courseDtoToMap.MinimalTresholdHomework = 99;
            courseDtoToMap.NumbersOfStudents = 10;
            courseDtoToMap.Students = new List<StudentDto>();
            courseDtoToMap.Students.Add(studentDto1);
            courseDtoToMap.Students.Add(studentDto2);

            expectedResult.Id = 125;
            expectedResult.CourseName = "Kurs";
            expectedResult.InstructorName = "Krzysztof";
            expectedResult.InstructorSurname = "Krawczyk";
            expectedResult.StartDate = new DateTime(05 / 05 / 2013);
            expectedResult.MinimalTresholdAttendance = 99;
            expectedResult.MinimalTresholdHomework = 99;
            expectedResult.NumbersOfStudents = 10;
            expectedResult.Students = new List<Student>();
            expectedResult.Students.Add(student1);
            expectedResult.Students.Add(student2);

            
            var courseMapped = DtoToEntityMapper.CourseDtoEntityModel(courseDtoToMap);
            Assert.AreEqual(expectedResult.Id, courseMapped.Id);
            Assert.AreEqual(expectedResult.CourseName, courseMapped.CourseName);
            Assert.AreEqual(expectedResult.InstructorName, courseMapped.InstructorName);
            Assert.AreEqual(expectedResult.InstructorSurname, courseMapped.InstructorSurname);
            Assert.AreEqual(expectedResult.StartDate, courseMapped.StartDate);
            Assert.AreEqual(expectedResult.MinimalTresholdAttendance, courseMapped.MinimalTresholdAttendance);
            Assert.AreEqual(expectedResult.MinimalTresholdHomework, courseMapped.MinimalTresholdHomework);
            Assert.AreEqual(expectedResult.NumbersOfStudents, courseMapped.NumbersOfStudents);
            CollectionAssert.Equals(expectedResult.Students, courseMapped.Students);
        }
    }

    }
