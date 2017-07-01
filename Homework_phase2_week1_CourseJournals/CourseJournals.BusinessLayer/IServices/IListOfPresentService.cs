using CourseJournals.BusinessLayer.Dtos;

namespace CourseJournals.BusinessLayer.Services
{
    public interface IListOfPresentService
    {
        bool AddListOfPresent(ListOfPresentDto listOfPresentDto);
    }
}