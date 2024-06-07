namespace BackEndASP.DTOs.ImageDTOs
{
    public class ImageBuidingDTO
    {

        public int Id { get; set; }
        public string ImageData64 { get; set; }


        public ImageBuidingDTO()
        {
            
        }

        public ImageBuidingDTO(Image entity)
        {
            this.Id = entity.Id;
            this.ImageData64 = entity.ImageData64;
        }

    }
}
