using CourseJournals.BusinessLayer.Dtos;

namespace CourseJournals.BusinessLayer.IServices
{
    public interface IListOfPresentService
    {
        bool AddListOfPresent(ListOfPresentDto listOfPresentDto);
    }
}