using System;
using System.Collections.Generic;
using System.Linq;
using CourseJournals.Cli.Helpers;
using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.Services;
using Ninject.Infrastructure.Language;

namespace CourseJournals.Cli
{
    internal class ProgramLoop : IProgramLoop
    {
        private IAttendanceService _attendanceService;
        private ICourseService _courseService;
        private IHomeworkService _homeworkService;
        private IListOfPresentService _listOfPresentService;
        private IStudentService _studentService;

        public delegate void GenerateReportFinishedEventHandler(object sender,
            GenerateReportFinishedEventArgs eventArgs);
        public event GenerateReportFinishedEventHandler GenerateReportFinished;


        public ProgramLoop(IAttendanceService attendanceService, ICourseService courseService,
            IHomeworkService homeworkService, IListOfPresentService listOfPresentService,
            IStudentService studentService)
        {
            _attendanceService = attendanceService;
            _courseService = courseService;
            _homeworkService = homeworkService;
            _listOfPresentService = listOfPresentService;
            _studentService = studentService;
        }

        public void Execute()
        {
            GenerateReportFinished += PrintReportInfo;
            GenerateReportFinished += AskForReportInfoExport;
            var exit = false;
            while (!exit)
            {
                var choice = ConsoleReadHelper.GetCommand("Select number: \n");
                var dictionaryOfCommand = new DictionaryOfCommands(this);
                var newDictionary = dictionaryOfCommand.CreateDictionary();

                if (newDictionary.ContainsKey(choice))
                {
                    newDictionary[choice].Invoke();
                }
                else if (choice == "6")
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Value '" + choice + "' is invalid - try again");
                }
            }
        }

        public void AddStudents()
        {
            Console.WriteLine("Provide information about the student:\n ");

            var student = new StudentDto();

            var exit = false;
            while (!exit)
            {
                long pesel = ConsoleReadHelper.GetLong("Personal ID number: ");
                if (!_studentService.CheckIfStudentsIsInTheDatabaseByPesel(pesel))
                {
                    student.Pesel = pesel;
                    exit = true;
                }
                else
                {
                    Console.WriteLine("The person with the given ID number is already in the datebase! Try again!");
                }
            }
            Console.Write("Student name: ");
            student.Name = Console.ReadLine();
            Console.Write("Student surname: ");
            student.Surname = Console.ReadLine();
            student.BirthDate = ConsoleReadHelper.GetDate("Student birthdate: (mm/dd/yyyy)");
            Console.Write("Student sex (Male/Female):");

            var correct = false;
            while (!correct)
            {
                var sex = Console.ReadLine();
                if (_studentService.CheckIfSexIsCorrectValue(sex))
                {
                    student.Sex = sex;
                    correct = true;
                }
                else
                {
                    Console.WriteLine("Incorrect value - try again! (Male or Female);");
                }
            }

            var success = false;
            try
            {
                success = _studentService.AddStudent(student);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when adding student: " + e.Message);
            }
            ConsoleWriteHelper.PrintOperationSuccessMessage(success);
        }

        public void AddCourses()
        {
            Console.WriteLine("Provide informations about course: ");

            var course = new CourseDto();
            var exit = false;
            while (!exit)
            {
                var courseId = ConsoleReadHelper.GetInt("Course ID:");
                if (!_courseService.CheckIfCourseIsInTheDatabaseById(courseId))
                {
                    course.Id = courseId;
                    exit = true;
                }
                else
                {
                    Console.WriteLine("The given ID is already in the datebase. Try again!");
                }
            }
            Console.WriteLine("Course name: ");
            course.CourseName = Console.ReadLine();
            Console.WriteLine("Instructor name: ");
            course.InstructorName = Console.ReadLine();
            Console.WriteLine("Instructor surname: ");
            course.InstructorSurname = Console.ReadLine();
            course.StartDate = ConsoleReadHelper.GetDate("Start date: ");
            Console.Write("Minimum number of homework points (%): ");
            course.MinimalTresholdHomework = ConsoleReadHelper.GetValidNumberOfPoints();
            Console.WriteLine("Minimum number of attendance points (%): ");
            course.MinimalTresholdAttendance = ConsoleReadHelper.GetValidNumberOfPoints();
            course.NumbersOfStudents = ConsoleReadHelper.GetInt("Number of students: ");
            Console.WriteLine("Add students to the course:\n");

            course.Students = new List<StudentDto>();
            var exit1 = false;
            while (!exit1)
            {
                long pesel = ConsoleReadHelper.GetLong("\nGive student's ID number you want to add to the course: ");
                if (!_studentService.CheckIfStudentsIsInTheDatabaseByPesel(pesel))
                {
                    Console.WriteLine("\n No student with a given pesel in the database or incorrect data entered");
                }
                else
                {
                    if (course.Students.Exists(x => x.Pesel == pesel))
                    {
                        Console.WriteLine("\nA student with a given ID has been added to this course. Add another student.");
                    }
                    else
                    {
                        course.Students.Add(_studentService.GetStudentData(pesel));
                        if (course.Students.Count == course.NumbersOfStudents)
                        {
                            Console.WriteLine("\nThe maximum number of students was reached!");
                            exit1 = true;
                        }
                    }
                }
            }

            var success = false;
            try
            {
                success = _courseService.AddCourse(course);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when adding course: " + e.Message);
            }
            ConsoleWriteHelper.PrintOperationSuccessMessage(success);
        }

        public void ChoosingCourse()
        {
            Console.WriteLine();


            Console.WriteLine("List of available courses: ");

            var allCourses = _courseService.GetAllCoursesNames();
            if (_courseService.GetAllCoursesNames().Count == 0 || _courseService.GetAllCoursesNames() == null)
            {
                Console.WriteLine("No courses added!");
            }
            else
            {
                string courseId = null;
                var exit = false;
                while (!exit)
                {
                    foreach (var course in allCourses)
                    {
                        Console.WriteLine(course.Key + " " + course.Value);
                    }
                    Console.Write("Select a course from the list above. Enter the course ID: ");
                    try
                    {
                        courseId = Console.ReadLine();
                        if (_courseService.CheckIfCourseIsInTheDatabaseById(Int32.Parse(courseId)))
                        {
                            ConsoleWriteHelper.PrintCourseOperations(courseId);
                            exit = true;

                        }
                        else
                        {
                            Console.WriteLine("\nWrong course ID - try again!\n");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\nWrong course ID - try again!\n");
                    }
                }
                var exit1 = false;
                while (!exit1)
                {
                    string choose = Console.ReadLine();
                    switch (choose)
                    {
                        case "1":
                            CheckAttendance(courseId);
                            exit1 = true;
                            break;
                        case "2":
                            CheckHomework(courseId);
                            exit1 = true;
                            break;
                        case "3":
                            PrintReport(courseId);
                            exit1 = true;
                            break;
                        case "4":
                            EditCourseData(courseId);
                            exit1 = true;
                            break;
                        case "5":
                            exit1 = true;
                            break;
                        default:
                            Console.Write("Value '" + choose + "' is invalid - try again. Choose action: ");
                            break;
                    }
                }
            }
        }

        public void PrintAllStudents()
        {
            var listAllStudents = _studentService.GetAllStudentsList();
            foreach (var students in listAllStudents)
            {
                Console.WriteLine("\n" + students.Pesel + " " + students.Name + " " + students.Surname + "\n");
            }
        }

        public void CheckAttendance(string courseId)
        {
            var exit = false;
            while (!exit)
            {
                var date = ConsoleReadHelper.GetDate("Give a date to check attendance: ");
                var attendance = new AttendanceDto();
                var listOfPresent = new ListOfPresentDto();
                if (_attendanceService.CheckIfDataIsInTheDatabase(date, courseId))
                {
                    Console.WriteLine("The given date was checked.");
                }
                else
                {
                    attendance.DayOfClass = date;
                    attendance.Courses = _courseService.GetCourseDataById(courseId);
                    var success1 = false;
                    try
                    {
                        success1 = _attendanceService.AddAttendance(attendance);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error when adding date: " + e.Message);
                    }
                    ConsoleWriteHelper.PrintOperationSuccessMessage(success1);
                    var studentsList = _studentService.GetStudentsList(courseId);
                    foreach (var student in studentsList)
                    {
                        StudentDto studencik = student;
                        Console.WriteLine(student.Pesel + " " + student.Name + " " + student.Surname + ": ");
                        var correct = false;
                        while (!correct)
                        {
                            string present = Console.ReadLine();
                            if (_attendanceService.CheckIfPresentIsCorrectValue(present))
                            {
                                listOfPresent.Attendance = _attendanceService.Attendance(courseId, date);
                                listOfPresent.Student = studencik;
                                listOfPresent.Present = present;
                                correct = true;
                            }
                            else
                            {
                                Console.WriteLine("Incorrect value - try again! (Present or absent: ");
                            }
                        }
                        var success = false;
                        try
                        {
                            success = _listOfPresentService.AddListOfPresent(listOfPresent);
                            exit = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error when adding student: " + e.Message);
                        }
                        ConsoleWriteHelper.PrintOperationSuccessMessage(success);
                    }
                }
            }
        }

        public void CheckHomework(string courseId)
        {
            Console.WriteLine("Give name of the homework or return to main menu('exit'):");
            var homework = new HomeworkDto();
            var exit = false;
            while (!exit)
            {
                var nameOfHomework = Console.ReadLine();
                if (nameOfHomework == "exit")
                    return;
                else if (_homeworkService.CheckIfTheHomeworkExists(nameOfHomework))
                {
                    Console.WriteLine("Homework with the given name exists. " +
                                      "Provide another name for homework or return to the main menu ('exit')");
                }
                else
                {
                    homework.HomeworkName = nameOfHomework;
                    var done = false;
                    while (!done)
                    {
                        try
                        {
                            Console.WriteLine("Maximum number of points: ");
                            homework.MaxPoints = double.Parse(Console.ReadLine());
                            done = true;
                        }
                        catch
                        {
                            Console.WriteLine("Incorrect value - try again!\n");
                        }
                    }
                    homework.Course = _courseService.GetCourseDataById(courseId);
                    var success1 = false;
                    try
                    {
                        success1 = _homeworkService.AddHomework(homework);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error when adding homework: " + e.Message);
                    }

                    ConsoleWriteHelper.PrintOperationSuccessMessage(success1);

                    var studentList = _studentService.GetStudentsList(courseId);
                    foreach (var student in studentList)
                    {
                        var homeworkMarks =
                            new HomeworkMarksDto
                            {
                                HomeworkDto = _homeworkService.GetHomeworkByName(nameOfHomework, courseId)
                            };
                        Console.WriteLine("\n" + student.Pesel + " " + student.Name + " " + student.Surname);
                        Console.WriteLine("Number of homework points by the student: ");
                        var working = false;
                        while (!working)
                        {
                            try
                            {
                                double homeworkPoints = double.Parse(Console.ReadLine());
                                if (homeworkPoints <= homework.MaxPoints && homeworkPoints >= 0)
                                {
                                    homeworkMarks.HomeworkPoints = homeworkPoints;
                                    working = true;
                                }
                                else
                                {
                                    Console.WriteLine("Number of points can not be greater " +
                                                      "than the maxium and must not be less than zero." +
                                                      " Try again.\nNumber of homework points" +
                                                      "by the student: ");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Wrong value - try again!");
                            }
                        }
                        homeworkMarks.Student = student;
                        var success = false;
                        try
                        {
                            success = _homeworkService.AddHomeworkMarks(homeworkMarks);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error when adding student: " + e.Message);
                        }

                        ConsoleWriteHelper.PrintOperationSuccessMessage(success);
                    }
                    exit = true;
                }
            }
        }
        public void PrintReport(string courseId)
        {
            var report = _courseService.GetReportInfo(courseId);
            OnGenerateReportFinished(report);
        }

        private void OnGenerateReportFinished(ReportDto report)
        {
            if (GenerateReportFinished == null)
            {
                return;
            }
            var eventArgs = new GenerateReportFinishedEventArgs();
            eventArgs.ReportDto = report;

            GenerateReportFinished(this, eventArgs);
        }


        private void PrintReportInfo(object sender, GenerateReportFinishedEventArgs eventArgs)
        {
            var report = eventArgs.ReportDto;
            Console.WriteLine($"Id: {report.CourseInfo.Id}\nCourse name: {report.CourseInfo.CourseName}\n" +
                              $"Instructor name: {report.CourseInfo.InstructorName}\n" +
                              $"Instructor surname: {report.CourseInfo.InstructorSurname}\n " +
                              $"Start date: {report.CourseInfo.StartDate}\n" +
                              $"Number of students: {report.CourseInfo.NumbersOfStudents}\n" +
                              $"Min. number of percents from attendances: {report.CourseInfo.MinimalTresholdAttendance}\n" +
                              $"Min. number of percents from homeworks: {report.CourseInfo.MinimalTresholdHomework}");

            Console.WriteLine("\n\nAttendance results: ");


            var studentsFromCourse = _studentService.GetStudentsList(report.CourseInfo.Id.ToString());
            if (studentsFromCourse.Count != 0)
            {
                foreach (var result in report.AttendanceResults)
                {
                    Console.WriteLine($"\n\nStudent info: {result.StudentsInfo}\n" +
                                      $"Points from attendance: {result.AttendancePoints}\n" +
                                      $"Max points: {result.MaxAttendancePoints}\n" +
                                      $"Points in percentage: {result.AttendancePercents}\n" +
                                      $"Ressult: {result.Results}");
                }
            }
            else
            {
                Console.WriteLine("No students assigned to the course");
            }

            Console.WriteLine("\n\nHomework results: ");
            if (studentsFromCourse.Count != 0)
            {
                foreach (var result in report.HomeworkResults)
                {
                    Console.WriteLine($"\nStudent info: {result.StudentsInfo}\n" +
                                      $"Points from attendance: {result.HomeworkPoints}\n" +
                                      $"Max points: {result.MaxHomeworkPoints}\n" +
                                      $"Points in percentage: {result.HomeworkPercents}\n" +
                                      $"Ressult: {result.Results}\n\n");
                }
            }
            else
            {
                Console.WriteLine("No students assigned to the course");
            }
        }

        public void AskForReportInfoExport(object sender, GenerateReportFinishedEventArgs eventargs)
        {

            Console.WriteLine("Do you want to export report data to files? (Y/N)");
            var choice = Console.ReadLine();
            while (choice != "Y" && choice != "N")
            {
                Console.WriteLine("Invalid value - try again!");
                choice = Console.ReadLine();
            }
            if (choice == "Y")
            {
                _courseService.SaveReportDataToFile(eventargs.ReportDto);
            }

        }

        public void EditCourseData(string id)
        {
            var newCourseDto = new CourseDto();

            CourseDto courseDto = _courseService.GetCourseDataById(id);
            Console.WriteLine("Enter current course data, or leave blank fields to keep the current data.\n");

            Console.Write("Course name: " + courseDto.CourseName + "   New course name: ");
            var newName = Console.ReadLine();
            newCourseDto.CourseName = string.IsNullOrEmpty(newName) ? courseDto.CourseName : newName;

            Console.Write("Instructor name: " + courseDto.InstructorName + "   New instructor name: ");
            var newInstructorName = Console.ReadLine();
            newCourseDto.InstructorName = string.IsNullOrEmpty(newInstructorName)
                ? courseDto.InstructorName
                : newInstructorName;

            Console.Write("Instructor surname: " + courseDto.InstructorSurname + "   New instructor surname: ");
            var newInstructorSurname = Console.ReadLine();
            newCourseDto.InstructorSurname = string.IsNullOrEmpty(newInstructorSurname) ? courseDto.InstructorSurname : newInstructorSurname;

            Console.Write("Minimum number of attendance points (%): " + courseDto.MinimalTresholdAttendance +
                "   New minimum number of attendance points (%): ");
            var newMinimalTresholdAttendance = Console.ReadLine();
            if (string.IsNullOrEmpty(newMinimalTresholdAttendance))
            {
                newCourseDto.MinimalTresholdAttendance = courseDto.MinimalTresholdAttendance;
            }
            else
            {
                newCourseDto.MinimalTresholdAttendance =
                    _courseService.GetValidNumbersOfPoints(newMinimalTresholdAttendance);
            }

            Console.Write("Minimum number of homework points (%): " + courseDto.MinimalTresholdHomework +
                "   New minimum number of homework points (%): ");
            var newMinimalTresholdHomework = Console.ReadLine();
            if (string.IsNullOrEmpty(newMinimalTresholdHomework))
            {
                newCourseDto.MinimalTresholdHomework = courseDto.MinimalTresholdHomework;
            }
            else
            {
                newCourseDto.MinimalTresholdHomework =
                    _courseService.GetValidNumbersOfPoints(newMinimalTresholdHomework);
            }
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Remove selected student from the course. Give id or leave" +
                                  "blank to keep all current students or stop removing students: ");
                var studentToRemove = Console.ReadLine();
                var studentList = _studentService.GetStudentsList(id);
                if (string.IsNullOrEmpty(studentToRemove))
                {
                    exit = true;
                }
                else if (studentList.Exists(s => s.Pesel == long.Parse(studentToRemove)))
                {
                    _courseService.RemoveStudentFromCourse(id, long.Parse(studentToRemove));
                    if (_courseService.GetCourseDataById(id).Students.Count != 0) continue;
                    Console.WriteLine("All students were removed from the course");
                    exit = true;
                }
                else
                {
                    Console.WriteLine("No student with given id on the list of students of the course!");
                }
            }
            newCourseDto.NumbersOfStudents = _courseService.GetCourseDataById(id).Students.Count;
            _courseService.ChangeCourseData(newCourseDto, id);
        }

        public void EditStudentData()
        {
            var exit = false;
            while (!exit)
            {
                var studentPesel = ConsoleReadHelper.GetLong("Provide ID number to edit student data: ");
                if (!_studentService.CheckIfStudentsIsInTheDatabaseByPesel(studentPesel))
                {
                    Console.WriteLine("\n No student with a given pesel in the database or incorrect data entered");
                }
                else
                {
                    StudentDto newStudentDto = new StudentDto();
                    StudentDto studentDto = _studentService.GetStudentData(studentPesel);

                    Console.WriteLine("Enter current student data, or leave blank fields to keep the current data.\n");

                    newStudentDto.Pesel = studentDto.Pesel;

                    Console.Write("Student name: " + studentDto.Name + "   New student name: ");
                    var newName = Console.ReadLine();
                    newStudentDto.Name = string.IsNullOrEmpty(newName) ? studentDto.Name : newName;

                    Console.WriteLine("Student surname: " + studentDto.Surname + "   New student surname: ");
                    var newSurname = Console.ReadLine();
                    newStudentDto.Surname = string.IsNullOrEmpty(newSurname) ? studentDto.Surname : newSurname;

                    Console.WriteLine("Student birthdate: " +
                                      studentDto.BirthDate + "   New student birthdate (mm/dd/yyyy): ");
                    var newBirthdate = Console.ReadLine();
                    if (string.IsNullOrEmpty(newBirthdate))
                    {
                        newStudentDto.BirthDate = studentDto.BirthDate;
                    }
                    else
                    {
                        newStudentDto.BirthDate = _studentService.GetDate(newBirthdate);
                    }
                    _studentService.ChangeStudentData(newStudentDto);

                    var exit1 = false;
                    while (!exit1)
                    {
                        StudentDto studentDto2 = _studentService.GetStudentData(studentPesel);
                        Console.WriteLine("List of student courses: \n");
                        foreach (var course in studentDto2.Courses)
                        {
                            Console.WriteLine(course.Id + " " + course.CourseName);
                        }
                        if (studentDto2.Courses.Count == 0)
                        {
                            Console.WriteLine("Student is not enrolled in any course!");
                            return;
                        }
                        Console.WriteLine("To remove a student from the course - " +
                                          "enter the course ID. Return to main menu - enter exit.");
                        var choose = Console.ReadLine();
                        try
                        {
                            if (choose == "exit")
                            {
                                exit = true;
                                exit1 = true;
                            }
                            else if (studentDto.Courses.Exists(s => s.Id == int.Parse(choose)))
                            {
                                _courseService.RemoveStudentFromCourse(choose, studentDto.Pesel);
                            }
                            else
                            {
                                Console.WriteLine("No course with given id!");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Value must be a number! Try again!");
                        }
                    }
                }
            }
        }
    }
}
