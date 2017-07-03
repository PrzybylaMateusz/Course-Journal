using System.Collections.Generic;
using System.Linq;
using CourseJournals.DataLayer.DbContexts;
using CourseJournals.DataLayer.Models;
using System.Data.Entity;
using CourseJournals.DataLayer.Interfaces;

namespace CourseJournals.DataLayer.Repositories
{
    public class HomeworkRepository : IHomeworkRepository
    {
        public List<Homework> GetHomeWorkByName(string name)
        {
            List<Homework> homeworks;
            using (var dbContext = new CourseJournalsDbContext())
            {
                homeworks = dbContext.HomeworkDbSet.Where(c => c.HomeworkName == name).ToList();
            }
            return homeworks;
        }

        public bool AddHomework(Homework homework)
        {
            Homework addedHomework;
            using (var dbContext = new CourseJournalsDbContext())
            {
                dbContext.HomeworkDbSet.Attach(homework);
                addedHomework = dbContext.HomeworkDbSet.Add(homework);
                dbContext.SaveChanges();
            }
            return addedHomework != null;
        }

        public List<Homework> GetHomeWorkByCourseId(int id)
        {
            List<Homework> homeworksByCourseId;
            using (var dbContext = new CourseJournalsDbContext())
            {
                homeworksByCourseId = dbContext.HomeworkDbSet.Where(c => c.Course.Id == id).ToList();
            }
            return homeworksByCourseId;
        }

        public bool AddHomeworkMarks(HomeworkMarks homeworkMarks)
        {
            HomeworkMarks addedHomeworkMarks;
            using (var dbContext = new CourseJournalsDbContext())
            {
                dbContext.HomeworkMarksDbSet.Attach(homeworkMarks);
                addedHomeworkMarks = dbContext.HomeworkMarksDbSet.Add(homeworkMarks);
                dbContext.SaveChanges();
            }
            return addedHomeworkMarks != null;
        }

        public List<HomeworkMarks> GetHomeWorkMarksByPesel(long pesel)
        {
            List<HomeworkMarks> list;
            using (var dbContext = new CourseJournalsDbContext())
            {
                list = dbContext.HomeworkMarksDbSet.Where(h => h.Student.Pesel == pesel)
                    .Include(a => a.Homework)
                    .Include(b => b.Student)
                    .ToList();
            }
            return list;
        }
    }
}
