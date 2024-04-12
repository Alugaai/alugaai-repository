using ApiCatalogo.Pagination;
using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.DTOs.PropertyDTOs;
using Correios.NET.Models;

namespace BackEndASP.Interfaces
{
    public interface IPropertyRepository
    {
        Task InsertProperty(PropertyInsertDTO dto, User user);
        Task<IEnumerable<PropertyFindAllBadgeDTO>> FindAllPropertiesAsync(PagePropertyQueryParams queryParams);
        Task<FindPropertyDetailsByIdDTO> FindPropertyDetailsById(int id);

    }
}
