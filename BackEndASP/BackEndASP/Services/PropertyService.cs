using ApiCatalogo.Pagination;
using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.DTOs.PropertyDTOs;
using BackEndASP.ExternalAPI;
using BackEndASP.ExternalAPI.GeoCoder;
using BackEndASP.Interfaces;
using BackEndASP.Utils;
using Correios.NET.Models;
using Geocoding;
using Geocoding.Google;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims;


namespace BackEndASP.Services
{
    public class PropertyService : IPropertyRepository
    {

        private readonly SystemDbContext _dbContext;

        public PropertyService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PropertyFindAllBadgeDTO>> FindAllPropertiesAsync(PagePropertyQueryParams queryParams)
        {
            double MinPrice = queryParams.MinPrice;
            double MaxPrice = queryParams.MaxPrice;

            List<Property> properties = await _dbContext.Properties
                .Where(p => p.Price >= MinPrice && p.Price <= MaxPrice)
                .ToListAsync();

            return properties.AsEnumerable().Select(p => new PropertyFindAllBadgeDTO(p)).ToList();
        }

        public async Task<FindPropertyDetailsByIdDTO> FindPropertyDetailsById(int id)
        {
            
            Property entity = await _dbContext.Properties
                .Include(p => p.Images)
                .Include(p => p.Owner)
                .ThenInclude(o => o.Image)
                .Include(p => p.StudentLikes)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id) ?? throw new ArgumentException("Resource not found");

            List<Image> selectedImages = entity.Images
                .Take(4)  // Seleciona apenas as primeiras 4 imagens
                .ToList();

            entity.Images = selectedImages;

            return new FindPropertyDetailsByIdDTO(entity);
        }

        public Task InsertProperty(PropertyInsertDTO dto, User user)
        {
             Property entity = new Property();
             copyDTOToEntity(dto, entity, user);

                
             var targetUrl = $"https://maps.googleapis.com/maps/api/geocode/json" +
               $"?address={ConvertAddress.Convert(dto)}" +
               $"&inputtype=textquery&fields=geometry" +
               $"&key={APIKey.key}";

             var json = new WebClient().DownloadString(targetUrl);
          

            GoogleGeoCodeResponse response = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(json);

            entity.Lat = response.results[0].geometry.location.lat;
            entity.Long = response.results[0].geometry.location.lng;

            _dbContext.Properties.Add(entity);
            return Task.CompletedTask;

        }

        private void copyDTOToEntity(PropertyInsertDTO dto, Property entity, User user)
        {
            entity.Address = dto.Address;
            entity.Neighborhood = dto.Neighborhood;
            entity.District = dto.District;
            entity.State = dto.State;
            entity.Number = dto.Number;
            entity.HomeComplement = dto.HomeComplement;
            entity.OwnerId = user.Id;
            entity.Bedrooms = dto.Bedrooms;
            entity.Bathrooms = dto.Bathrooms;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.Name = dto.Name;
        }

        


        
    }
}
