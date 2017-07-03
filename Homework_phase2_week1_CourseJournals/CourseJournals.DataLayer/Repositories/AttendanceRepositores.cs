using CourseJournals.DataLayer.DbContexts;
using CourseJournals.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CourseJournals.DataLayer.Interfaces;

namespace CourseJournals.DataLayer.Repositories
{
    public class AttendanceRepositores : IAttendanceRepositores
    {
        public List<Attendance> GetDayInSystem(DateTime date)
        {
            List<Attendance> days;
            using (var dbContext = new CourseJournalsDbContext())
            {
                days = dbContext.AttendanceDbSet.Where(a => a.DayOfClass == date).Include(b => b.Courses).ToList();
            }
            return days;
        }

        public bool AddAttendance(Attendance attendance)
        {
            Attendance addedAttendance;
            using (var dbContext = new CourseJournalsDbContext())
            {
                dbContext.AttendanceDbSet.Attach(attendance);
                addedAttendance = dbContext.AttendanceDbSet.Add(attendance);
                dbContext.SaveChanges();
            }
            return addedAttendance != null;
        }

        public List<Attendance> GetAllDays()
        {
            List<Attendance> allDays;
            using (var dbContext = new CourseJournalsDbContext())
            {
                allDays = dbContext.AttendanceDbSet.ToList();
            }
            return allDays;
        }

        public List<Attendance> GetAttendanceDays(int id)
        {
            List<Attendance> list;
            using (var dbContext = new CourseJournalsDbContext())
            {
                list = dbContext.AttendanceDbSet.Where(a => a.Courses.Id == id).Include(b => b.Courses).ToList();
            }
            return list;
        }

        public List<ListOfPresent> GetListOfAllPresentsAtCourse(long pesel)
        {
            List<ListOfPresent> list;
            using (var dbContext = new CourseJournalsDbContext())
            {
                list = dbContext.ListOfPresentDbSet.Where(a => a.Student.Pesel == pesel).Include(b => b.Attendance).Include(c => c.Student).ToList();
            }
            return list;
        }
    }
}

