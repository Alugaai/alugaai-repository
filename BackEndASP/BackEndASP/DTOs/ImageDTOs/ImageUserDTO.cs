namespace BackEndASP.DTOs.ImageDTOs
{
    public class ImageUserDTO
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }



        public ImageUserDTO()
        {
            
        }

        public ImageUserDTO(Image entity)
        {
            this.Id = entity.Id;
            this.ImagePath = entity.ImagePath;
        }
    }
}
