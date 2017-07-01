using System;

namespace CourseJournals.Cli.Helpers
{
    internal class ConsoleReadHelper
    {
        public static int GetInt(string message)
        {
            int number;
            Console.WriteLine(message);
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Value must be a number - try again!");
            }
            return number;
        }

        public static int GetValidNumberOfPoints()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Value must be a number - try again!");
            }
            if (number < 0 || number > 100)
            {
                Console.WriteLine("The value should be between 0 and 100 - try again");
            }
            return number;
        }

        public static string GetCommand(string message)
        {
            Console.Write(message + "\n 1. Add student to student database.\n 2. Add new course.\n" +
                          " 3. Choose a course.\n 4. List all students in the student database.\n " +
                          "5. Edit student data.\n 6. Exit.\n\n");

            var choice = Console.ReadLine();
            return choice;
        }

        public static long GetLong(string message)
        {
            long pesel;
            Console.Write(message);
            while (!long.TryParse(Console.ReadLine(), out pesel))
            {
                Console.WriteLine("Invalid value - try again!");
            }
            return pesel;
        }
        public static DateTime GetDate(string message)
        {
            DateTime date;
            Console.WriteLine(message);
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Wrong date format - try again! (dd/mm/yyyy)");
            }
            return date;
        }
    }
}
