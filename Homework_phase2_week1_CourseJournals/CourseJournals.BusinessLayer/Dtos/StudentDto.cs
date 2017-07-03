using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseJournals.BusinessLayer.Dtos
{
    public class StudentDto
    {
        [Key]
        public long Pesel;
        public string Name;
        public string Surname;
        public DateTime BirthDate;
        public string Sex;
        public List<CourseDto> Courses;
    }
}

