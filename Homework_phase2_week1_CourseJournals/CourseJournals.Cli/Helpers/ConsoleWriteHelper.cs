using System;


namespace CourseJournals.Cli.Helpers
{
    internal class ConsoleWriteHelper
    {
        public static void PrintOperationSuccessMessage(bool success)
        {
            Console.WriteLine(success ? "\nOperation succeded!\n" : "\nOperation failed!\n");
        }

        public static void PrintCourseOperations(string courseId)
        {
           Console.WriteLine("\nYou have selected a course with ID "+courseId+".Choose action:\n" +
                             "1.Check attendance.\n" +
                             "2.Check homework.\n" +
                             "3.View course report.\n" +
                             "4.Edit course data.\n"+
                             "5.Return to main menu.\n");
        }
        public static void PrintOperationSuccessMessage1(bool success1)
        {
            Console.WriteLine(success1 ? " " : "\nSomething went wrong!\n");
        }
    }
}
