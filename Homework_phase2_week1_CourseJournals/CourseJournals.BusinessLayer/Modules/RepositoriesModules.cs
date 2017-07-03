using CourseJournals.DataLayer.Interfaces;
using CourseJournals.DataLayer.Repositories;
using Ninject.Modules;

namespace CourseJournals.BusinessLayer.Modules
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
