using House.Common.EntityModels.PostgreSQL.Packt.Shared;

namespace HousingAnalysis.ApiServer.Repository.Interfaces
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User?> GetByEmail(string email);
    }
}
