using System.Collections.Generic;

namespace CourseJournals.BusinessLayer.Dtos
{
    public class ReportDto
    {
        public CourseDto CourseInfo;
        public List<AttendanceResultDto> AttendanceResults;
        public List<HomeworkResultDto> HomeworkResults;
    }
}
