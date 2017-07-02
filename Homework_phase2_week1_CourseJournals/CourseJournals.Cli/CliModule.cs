using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
