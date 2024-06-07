namespace BackEndASP.DTOs.ImageDTOs
{
    public class ImageUserDTO
    {
        public int Id { get; set; }
        public string ImageData64 { get; set; }



        public ImageUserDTO()
        {
            
        }

        public ImageUserDTO(Image entity)
        {
            this.Id = entity.Id;
            this.ImageData64 = entity.ImageData64;
        }
    }
}
