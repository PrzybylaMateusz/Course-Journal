using System;
using System.Collections.Generic;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.Mappers;
using CourseJournals.DataLayer;
using CourseJournals.DataLayer.Repositories;

namespace CourseJournals.BusinessLayer.Services
{
    public class CourseService : ICourseService
    {
        private ICoursesRepository _courseRepository;

        public CourseService(ICoursesRepository coursesRepository)
        {
            _courseRepository = coursesRepository;
        }

        public bool AddCourse(CourseDto courseDto)
        {
            if (CheckIfCourseIsInTheDatabaseById(courseDto.Id))
            {
                throw new Exception("The course is already in the database!");
            }
            var course = DtoToEntityMapper.CourseDtoEntityModel(courseDto);
            return _courseRepository.AddCourse(course);
        }

        public bool CheckIfCourseIsInTheDatabaseById(int id)
        {
            var courses = _courseRepository.GetCoursesById(id);
            return courses != null && courses.Count != 0;
        }

        public Dictionary<int, string> GetAllCoursesNames()
        {
            var result = new Dictionary<int, string>();
            var allCourses = _courseRepository.GetAllCourses();
            foreach (var course in allCourses)
            {
                result.Add(course.Id, course.CourseName);
            }
            return result;
        }

        public CourseDto GetCourseDataById(string courseId)
        {
            var course = _courseRepository.GetCoursesDataById(Int32.Parse(courseId));

            return EntityToDtoMapper.CourseEntityModelToDto(course);
        }

        public int GetValidNumbersOfPoints(string numberOfPoints)
        {
            var number = 0;
            var exit = false;
            while (!exit)
            {
                while (!int.TryParse(numberOfPoints, out number))
                {
                    Console.WriteLine("Value must be a number - try again!");
                    numberOfPoints = Console.ReadLine();
                }
                if (number < 0 || number > 100)
                {
                    Console.WriteLine("The value should be between 0 and 100 - try again. Get number: ");
                    numberOfPoints = Console.ReadLine();
                }
                else
                {
                    exit = true;
                }
            }
            return number;
        }

        public void ChangeCourseData(CourseDto courseDto, string id)
        {
            _courseRepository.ChangeCourseData(DtoToEntityMapper.CourseDtoEntityModel(courseDto), Int32.Parse(id));
        }

        public void RemoveStudentFromCourse(string id, long pesel)
        {
            _courseRepository.RemoveStudentFromCourse(Int32.Parse(id), pesel);
        }
    }
}
