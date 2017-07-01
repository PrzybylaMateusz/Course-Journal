using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseJournals.DataLayer.Repositories;
using Ninject.Modules;

namespace CourseJournals.BusinessLayer
{
    public class RepositoriesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAttendanceRepositores>().To<AttendanceRepositores>();
            Bind<ICoursesRepository>().To<CoursesRepository>();
            Bind<IHomeworkRepository>().To<HomeworkRepository>();
            Bind<IListOfPresentRepository>().To<ListOfPresentRepository>();
            Bind<IStudentsRepository>().To<StudentsRepository>();
        }
    }
}
