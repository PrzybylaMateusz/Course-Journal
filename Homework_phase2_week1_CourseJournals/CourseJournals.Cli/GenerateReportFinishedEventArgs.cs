using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseJournals.BusinessLayer.Dtos;

namespace CourseJournals.Cli
{
    public class GenerateReportFinishedEventArgs : EventArgs
    {
        public ReportDto ReportDto;
    }
}
