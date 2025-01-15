using Packt.Shared;

namespace HousingAnalysis.ApiServer.Repository
{
    public class HouseRepository: GenericRepository<Property>
    {
        public HouseRepository(HouseDbContext context) : base(context)
        {
        }
    }
}
