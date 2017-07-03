using System;
using CourseJournals.BusinessLayer.Dtos;

namespace CourseJournals.Cli
{
    public class GenerateReportFinishedEventArgs : EventArgs
    {
        public ReportDto ReportDto;
    }
}
