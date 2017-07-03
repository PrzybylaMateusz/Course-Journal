using System;
using System.Linq;
using CourseJournals.DataLayer.DbContexts;
using CourseJournals.DataLayer.Interfaces;
using CourseJournals.DataLayer.Models;

namespace CourseJournals.DataLayer.Repositories
{
    public class ListOfPresentRepository : IListOfPresentRepository
    {
        public Attendance GetAttendanceDataByIdDate(int courseId, DateTime date)
        {
            using (var dbContext = new CourseJournalsDbContext())
            {
                var asd = dbContext.AttendanceDbSet.Where(b => b.DayOfClass == date).ToList();
                var attendance = asd.First(n => n.Courses.Id == courseId);
                return attendance;
            }
        }

        public bool AddListOfPresent(ListOfPresent listOfPresent)
        {
            ListOfPresent addedListOfPresent;
            using (var dbContext = new CourseJournalsDbContext())
            {
                dbContext.ListOfPresentDbSet.Attach(listOfPresent);
                addedListOfPresent = dbContext.ListOfPresentDbSet.Add(listOfPresent);
                dbContext.SaveChanges();
            }
            return addedListOfPresent != null;
        }
    }
}
