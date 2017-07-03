using CourseJournals.BusinessLayer.IServices;
using CourseJournals.BusinessLayer.Services;
using Ninject.Modules;

namespace CourseJournals.BusinessLayer.Modules
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAttendanceService>().To<AttendanceService>();
            Bind<ICourseService>().To<CourseService>();
            Bind<IHomeworkService>().To<HomeworkService>();
            Bind<IListOfPresentService>().To<ListOfPresentService>();
            Bind<IStudentService>().To<StudentService>();
        }
    }
}
