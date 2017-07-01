using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseJournals.BusinessLayer.Services;
using Ninject.Modules;
using CourseJournals.DataLayer;

namespace CourseJournals.BusinessLayer
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
