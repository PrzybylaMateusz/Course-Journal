using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseJournals.BusinessLayer.Dtos
{
    public class CourseDto
    {
        [Key]
        public int Id;
        public string CourseName;
        public string InstructorName;
        public string InstructorSurname;
        public DateTime StartDate;
        public double MinimalTresholdHomework;
        public double MinimalTresholdAttendance;
        public int NumbersOfStudents;
        public List<StudentDto> Students;
    }
}
