using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseJournals.BusinessLayer.Dtos
{
    public class ReportDto
    {
        public CourseDto CourseInfo;
        public List<AttendanceResultDto> AttendanceResults;
        public List<HomeworkResultDto> HomeworkResults;

    }

    
}
