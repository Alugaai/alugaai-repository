namespace BackEndASP.DTOs.ImageDTOs
{
    public class ImageBuidingDTO
    {

        public int Id { get; set; }
        public string ImagePath { get; set; }


        public ImageBuidingDTO()
        {
            
        }

        public ImageBuidingDTO(Image entity)
        {
            this.Id = entity.Id;
            this.ImagePath = entity.ImagePath;
        }

    }
}
