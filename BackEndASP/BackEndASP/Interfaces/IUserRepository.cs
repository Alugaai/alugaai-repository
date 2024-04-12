namespace BackEndASP.Interfaces
{
    public interface IUserRepository
    {

        Task<dynamic> FindUserByEmail(string email);

    }
}
