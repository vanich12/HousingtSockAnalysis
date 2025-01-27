using HousingAnalysis.ApiServer.Extension;
using Packt.Shared;

namespace HousingAnalysis.ApiServer.Repository.Interfaces
{
    public interface IHousePropertyRepository: IGenericRepository<Property>
    {
        /// <summary>
        /// Пагинация предложений о продаже жилья
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedPage<Property>> GetHousesByPage(int page, int pageSize);
    }
}
