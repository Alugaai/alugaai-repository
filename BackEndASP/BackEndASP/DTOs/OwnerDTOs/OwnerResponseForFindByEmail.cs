using BackEndASP.DTOs.PropertyDTOs;
using BackEndASP.Entities;

namespace BackEndASP.DTOs
{
    public class OwnerResponseForFindByEmail : UserFindAllInfoDTO
    {
        public ICollection<PropertyDTO>? Properties { get; set; }


        public OwnerResponseForFindByEmail()
        {
            
        }

        public OwnerResponseForFindByEmail(Owner entity) : base(entity) 
        {
            this.Properties = entity.Properties != null ? entity.Properties.Select(p => new PropertyDTO(p)).ToList() : null;
        }


    }
}
