using Ninject.Modules;

namespace CourseJournals.Cli
{
    public class CliModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProgramLoop>().To<ProgramLoop>();
        }
    }
}
