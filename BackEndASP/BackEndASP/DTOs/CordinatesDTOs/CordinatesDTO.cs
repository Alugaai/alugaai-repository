namespace BackEndASP.DTOs.CordinatesDTOs
{
    public class CordinatesDTO
    {
        public double Lat { get; set; }
        public double Lng { get; set; }

        public CordinatesDTO()
        {
        }

        public CordinatesDTO(string lat, string lng)
        {
            Lat = double.Parse(lat);
            Lng = double.Parse(lng); ;
        }
    }
}
