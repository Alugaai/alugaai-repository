using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.Interfaces;
using ViaCep;

namespace BackEndASP.Services
{
    public class BuildingService : IBuildingRepository
    {
        public async Task<BuildingResponseDTO> GetAddressByCep(string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                throw new ArgumentNullException(nameof(cep), "CEP cannot be null or empty");
            }

            try
            {
                var address = new ViaCepClient().Search(cep);

                if (address == null)
                {
                    throw new Exception("Cep does not exist");
                }

                var building = new Building
                {
                    Address = address.Street,
                    State = address.StateInitials,
                    District = address.City,
                    Neighborhood = address.Neighborhood
                };

                return new BuildingResponseDTO(building);
            }
            catch (ArgumentNullException argEx)
            {
                // Log or handle specific ArgumentNullException
                throw new Exception("Invalid argument passed to the method", argEx);
            }
            catch (Exception ex)
            {
                // Log the exception details for further investigation
                throw new Exception("Failed to call the API", ex);
            }
        }
    }
}
