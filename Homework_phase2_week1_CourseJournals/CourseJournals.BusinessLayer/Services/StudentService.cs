using System;
using System.Collections.Generic;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.Mappers;
using CourseJournals.DataLayer.Repositories;

namespace CourseJournals.BusinessLayer.Services
{
    public class StudentService : IStudentService
    {
        private IStudentsRepository _studentsRepository;

        public StudentService(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public bool AddStudent(StudentDto studentDto)
        {
            if (CheckIfStudentsIsInTheDatabaseByPesel(studentDto.Pesel))
            {
                throw new Exception("The student is already in the database!");
            }
            var student = DtoToEntityMapper.StudentDtoEntityModel(studentDto);
            return _studentsRepository.AddStudent(student);
        }

        public bool CheckIfStudentsIsInTheDatabaseByPesel(long pesel)
        {
            var students = _studentsRepository.GetStudentsByPesel(pesel);
            return students != null && students.Count != 0;
        }

        public bool CheckIfSexIsCorrectValue(string sex)
        {
            return sex == "Male" || sex == "Female";
        }

        public StudentDto GetStudentData(long pesel)
        {
            var student = _studentsRepository.GetSpecificStudentByPesel(pesel);
            var studentDto = EntityToDtoMapper.StudentEntityModelToDto(student);
            studentDto.Courses = EntityToDtoMapper.ListOfCoursesToListOfCoursesDto(student.Courses);
            return studentDto;
        }

        public List<StudentDto> GetStudentsList(string courseId)
        {
            var courseStudents = new List<StudentDto>();
            var studentOnCourse = _studentsRepository.GetStudents(Int32.Parse(courseId));
            foreach (var student in studentOnCourse)
            {
                StudentDto studentDto = EntityToDtoMapper.StudentEntityModelToDto(student);
                courseStudents.Add(studentDto);
            }
            return courseStudents;
        }

        public List<StudentDto> GetAllStudentsList()
        {
            var allStudentList = new List<StudentDto>();
            var allStudentListEntity = _studentsRepository.GetAllStudents();
            foreach (var student in allStudentListEntity)
            {
                StudentDto studentDto = EntityToDtoMapper.StudentEntityModelToDto(student);
                allStudentList.Add(studentDto);
            }
            return allStudentList;
        }

        public DateTime GetDate(string newBirthdate)
        {
            DateTime date;
            while (!DateTime.TryParse(newBirthdate, out date))
            {
                Console.WriteLine("Wrong date format - try again! (mm/dd/yyyy)");
                newBirthdate = Console.ReadLine();
            }
            return date;
        }

        public void ChangeStudentData(StudentDto studentDto)
        {
            _studentsRepository.ChangeStudentData(DtoToEntityMapper.StudentDtoEntityModel(studentDto));
        }
    }
}
