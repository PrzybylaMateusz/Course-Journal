using CourseJournals.BusinessLayer.Dtos;
using CourseJournals.BusinessLayer.IServices;
using CourseJournals.BusinessLayer.Mappers;
using CourseJournals.DataLayer.Interfaces;
using CourseJournals.DataLayer.Repositories;

namespace CourseJournals.BusinessLayer.Services
{
    public class ListOfPresentService : IListOfPresentService
    {
        private readonly IListOfPresentRepository _listOfPresentRepository;

        public ListOfPresentService(IListOfPresentRepository listOfPresentRepository)
        {
            _listOfPresentRepository = listOfPresentRepository;
        }

        public bool AddListOfPresent(ListOfPresentDto listOfPresentDto)
        {
            var listOfPresent = DtoToEntityMapper.PresentDtoToPresent(listOfPresentDto);
            return _listOfPresentRepository.AddListOfPresent(listOfPresent);
        }
    }
}
