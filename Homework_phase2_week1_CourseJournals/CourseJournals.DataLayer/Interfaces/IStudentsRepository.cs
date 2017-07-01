using System.Collections.Generic;
using CourseJournals.DataLayer.Models;

namespace CourseJournals.DataLayer.Repositories
{
    public interface IStudentsRepository
    {
        bool AddStudent(Student student);
        List<Student> GetStudentsByPesel(long pesel);
        Student GetSpecificStudentByPesel(long pesel);
        List<Student> GetAllStudentsList(string courseId);
        List<Student> GetStudents(int id);
        List<Student> GetAllStudents();
        void ChangeStudentData(Student student);
    }
}