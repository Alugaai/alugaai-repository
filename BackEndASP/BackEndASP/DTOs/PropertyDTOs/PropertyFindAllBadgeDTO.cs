
using BackEndASP.DTOs.CordinatesDTOs;

namespace BackEndASP.DTOs.PropertyDTOs
{
    public class PropertyFindAllBadgeDTO
    {

        public int Id { get; set; }
        public double? Price { get; set; }
        
        public CordinatesDTO Position { get; set; }

        public PropertyFindAllBadgeDTO()
        {
            
        }

        public PropertyFindAllBadgeDTO(Property property)
        {
            this.Id = property.Id;
            this.Price = property.Price;
            this.Position = new CordinatesDTO(property.Lat, property.Long);
        }

    }
}
