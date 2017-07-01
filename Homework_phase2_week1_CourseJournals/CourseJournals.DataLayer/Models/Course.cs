using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseJournals.DataLayer.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string InstructorName { get; set; }
        public string InstructorSurname { get; set; }
        public DateTime StartDate { get; set; }
        public double MinimalTresholdHomework { get; set; }
        public double MinimalTresholdAttendance { get; set; }
        public int NumbersOfStudents { get; set; }
        public List<Student> Students { get; set; }
    }
}
