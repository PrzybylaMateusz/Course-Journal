using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace CourseJournals.Cli
{
    internal class DictionaryOfCommands
    {
        private IProgramLoop _programLoop;

        public DictionaryOfCommands(IProgramLoop programLoop)
        {
            _programLoop = programLoop;
        }

        public delegate void Command();
        public Dictionary<string, Command> CreateDictionary()
        {
            Dictionary<string, Command> dictionaryOfCommands = new Dictionary<string, Command>();

            dictionaryOfCommands["1"] = _programLoop.AddStudents;
            dictionaryOfCommands["2"] = _programLoop.AddCourses;
            dictionaryOfCommands["3"] = _programLoop.ChoosingCourse;
            dictionaryOfCommands["4"] = _programLoop.PrintAllStudents;
            dictionaryOfCommands["5"] = _programLoop.EditStudentData;
            
            return dictionaryOfCommands;
        }
    }
}
