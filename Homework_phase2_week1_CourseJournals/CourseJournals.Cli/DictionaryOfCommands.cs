using System.Collections.Generic;

namespace CourseJournals.Cli
{
    internal class DictionaryOfCommands
    {
        private readonly IProgramLoop _programLoop;

        public DictionaryOfCommands(IProgramLoop programLoop)
        {
            _programLoop = programLoop;
        }

        public delegate void Command();
        public Dictionary<string, Command> CreateDictionary()
        {
            var dictionaryOfCommands = new Dictionary<string, Command>
            {
                ["1"] = _programLoop.AddStudents,
                ["2"] = _programLoop.AddCourses,
                ["3"] = _programLoop.ChoosingCourse,
                ["4"] = _programLoop.PrintAllStudents,
                ["5"] = _programLoop.EditStudentData
            };
            return dictionaryOfCommands;
        }
    }
}