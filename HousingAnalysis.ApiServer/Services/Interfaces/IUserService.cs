using HousingAnalysis.ApiServer.Repository.Interfaces;
using HousingAnalysis.ApiServer.Utilities;

namespace HousingAnalysis.ApiServer.Services.Interfaces
{
    public interface IUserService
    {
        Task Register(string userName, string email, string password);
        Task<string> LogIn(string email, string password);
    }
}