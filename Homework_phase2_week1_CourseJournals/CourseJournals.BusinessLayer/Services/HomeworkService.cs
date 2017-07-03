using System;
using System.Collections.Generic;
using System.Linq;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.IServices;
using CourseJournals.BusinessLayer.Mappers;
using CourseJournals.DataLayer.Interfaces;
using CourseJournals.DataLayer.Repositories;

namespace CourseJournals.BusinessLayer.Services
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IHomeworkRepository _homeworkRepository;
        public HomeworkService(IHomeworkRepository homeworkRepository)
        {
            _homeworkRepository = homeworkRepository;
        }

        public HomeworkService()
        {
        }

        public bool CheckIfTheHomeworkExists(string name)
        {
            var homework = _homeworkRepository.GetHomeWorkByName(name);
            return homework != null && homework.Count != 0;
        }

        public bool AddHomework(HomeworkDto homeworkDto)
        {
            var homework = DtoToEntityMapper.HomeworkDtotoHomework(homeworkDto);
            return _homeworkRepository.AddHomework(homework);
        }

        public List<HomeworkDto> GetListOfHomework(string courseId)
        {
            var list = _homeworkRepository.GetHomeWorkByCourseId(int.Parse(courseId));
            return list.Select(EntityToDtoMapper.HomeworkEntityModelToDto).ToList();
        }

        public double MaxPoints(List<HomeworkDto> listOfHomeworks)
        {
            return listOfHomeworks.Select(record => record.MaxPoints).Aggregate<double, double>(0, (current, i) => current + i);
        }

        public double CalculateStudentHomeworkPoints(List<HomeworkDto> listOfHomeworks, long pesel, List<HomeworkMarksDto> list)
        {
            double studentHomeworkPoints = 0;
            foreach (var homework in listOfHomeworks)
            {
                foreach (var record in list)
                {
                    if (record.Student.Pesel == pesel && record.HomeworkDto.Id == homework.Id)
                    {
                        studentHomeworkPoints = studentHomeworkPoints + record.HomeworkPoints;
                    }
                }
            }
            return studentHomeworkPoints;
        }

        public bool AddHomeworkMarks(HomeworkMarksDto homeworkMarksDto)
        {
            var homeworkMark = DtoToEntityMapper.HomeworkMarksDtotoHomeworkMarks(homeworkMarksDto);
            return _homeworkRepository.AddHomeworkMarks(homeworkMark);
        }

        public HomeworkDto GetHomeworkByName(string name, string id)
        {
            var homework = new HomeworkDto();
            var list = GetListOfHomework(id);
            foreach (var home in list)
            {
                if (home.HomeworkName == name)
                {
                    homework = home;
                }
            }
            return homework;
        }

        public List<HomeworkMarksDto> GetListOfHomeworkMarks(long pesel)
        {
            var list = _homeworkRepository.GetHomeWorkMarksByPesel(pesel);
            return list.Select(EntityToDtoMapper.HomeworkMarksEntityModelToDto).ToList();
        }
    }
}
