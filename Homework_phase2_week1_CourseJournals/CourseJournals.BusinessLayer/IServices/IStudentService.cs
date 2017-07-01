using System;
using System.Collections.Generic;
using CourseJournals.BusinessLayer.Dtos;

namespace CourseJournals.BusinessLayer.Services
{
    public interface IStudentService
    {
        bool AddStudent(StudentDto studentDto);
        bool CheckIfStudentsIsInTheDatabaseByPesel(long pesel);
        bool CheckIfSexIsCorrectValue(string sex);
        StudentDto GetStudentData(long pesel);
        List<StudentDto> GetStudentsList(string courseId);
        List<StudentDto> GetAllStudentsList();
        DateTime GetDate(string newBirthdate);
        void ChangeStudentData(StudentDto studentDto);
    }
}