using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CourseJournals.DataLayer.DbContexts;
using CourseJournals.DataLayer.Interfaces;
using CourseJournals.DataLayer.Models;

namespace CourseJournals.DataLayer.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        public bool AddStudent(Student student)
        {
            Student addedStudent;
            using (var dbContext = new CourseJournalsDbContext())
            {
                addedStudent = dbContext.StudentsDbSet.Add(student);
                dbContext.SaveChanges();
            }
            return addedStudent != null;
        }

        public List<Student> GetStudentsByPesel(long pesel)
        {
            List<Student> students;
            using (var dbContext = new CourseJournalsDbContext())
            {
                students = dbContext.StudentsDbSet.Where(s => s.Pesel == pesel).ToList();
            }
            return students;
        }

        public Student GetSpecificStudentByPesel(long pesel)
        {
            Student student;
            using (var dbContext = new CourseJournalsDbContext())
            {
                var studentList = dbContext.StudentsDbSet.Include(c => c.Courses).ToList();
                student = studentList.First(x => x.Pesel == pesel);
            }
            return student;
        }

        public List<Student> GetAllStudentsList(string courseId)
        {
            var courseStudents = new List<Student>();
            using (var dbContext = new CourseJournalsDbContext())
            {
                var students = dbContext.StudentsDbSet.ToList();
                foreach (var student in students)
                {
                    courseStudents.AddRange(student.Courses.Select(course => course.Id == int.Parse(courseId) ? student : null));
                }
            }
            return courseStudents;
        }

        public List<Student> GetStudents(int id)
        {
            var list = new List<Student>();
            using (var dbContext = new CourseJournalsDbContext())
            {
                foreach (var student in dbContext.CoursesDbSet.Where(a => a.Id == id).Include(c => c.Students))
                {
                    list.AddRange(student.Students);
                }
            }
            return list;
        }

        public List<Student> GetAllStudents()
        {
            var list = new List<Student>();
            using (var dbContext = new CourseJournalsDbContext())
            {
                list.AddRange(dbContext.StudentsDbSet);
            }
            return list;
        }

        public void ChangeStudentData(Student student)
        {
            using (var dbContext = new CourseJournalsDbContext())
            {
                var specyficStudent = dbContext.StudentsDbSet.First(s => s.Pesel == student.Pesel);
                specyficStudent.Name = student.Name;
                specyficStudent.Surname = student.Surname;
                specyficStudent.BirthDate = student.BirthDate;
                dbContext.SaveChanges();
            }
        }
    }
}
