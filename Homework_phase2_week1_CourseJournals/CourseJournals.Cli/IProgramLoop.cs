namespace CourseJournals.Cli
{
    internal interface IProgramLoop
    {
        void Execute();
        void AddStudents();
        void AddCourses();
        void ChoosingCourse();
        void PrintAllStudents();
        void CheckAttendance(string courseId);
        void CheckHomework(string courseId);
        void PrintReport(string courseId);
        void EditCourseData(string id);
        void EditStudentData();
    }
}
