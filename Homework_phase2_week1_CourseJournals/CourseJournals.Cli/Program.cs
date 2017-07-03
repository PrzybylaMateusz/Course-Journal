using CourseJournals.BusinessLayer;
using CourseJournals.BusinessLayer.Modules;
using Ninject;

namespace CourseJournals.Cli
{
    internal class Program
    {
        private static void Main()
        {
            IKernel kernel = new StandardKernel(new ServicesModule(), new CliModule(),
                new RepositoriesModule());
            kernel.Get<ProgramLoop>().Execute();
        }
    }
}
