using HousingAnalysis.ApiServer.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;

namespace HousingAnalysis.ApiServer.Repository
{
    public class HouseRepository : GenericRepository<Property>
    {
        #region Поля и свойства

        private readonly HouseDbContext _context;

        #endregion

        public HouseRepository(HouseDbContext context) : base(context)
        {
        }

        public async Task<PagedPage<Property>> GetHousesByPage(int pageNumber, int pageSize)
        {
            IQueryable<Property> source = this._context.Properties;
            return await PagedPage<Property>.ToPagedPage<decimal?>(source, pageNumber, pageSize, x => x.PricePerMonth);
        }
    }
}