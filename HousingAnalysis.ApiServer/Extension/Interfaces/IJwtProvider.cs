using House.Common.EntityModels.PostgreSQL.Packt.Shared;

namespace HousingAnalysis.ApiServer.Extension.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User? user);
    }
}
