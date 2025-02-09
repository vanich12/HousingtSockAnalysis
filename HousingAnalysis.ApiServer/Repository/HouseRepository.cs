using House.Common.EntityModels.PostgreSQL.Packt.Shared;
using HousingAnalysis.ApiServer.Extension;
using HousingAnalysis.ApiServer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;

namespace HousingAnalysis.ApiServer.Repository
{
    public class HouseRepository : GenericRepository<Property>,IHousePropertyRepository
    {
        #region Поля и свойства

        private readonly HouseDbContext _context;

        #endregion

        public HouseRepository(HouseDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<PagedPage<Property>> GetHousesByPage(int pageNumber, int pageSize)
        {
            IQueryable<Property> source = this._context.Properties;
            return await PagedPage<Property>.ToPagedPage<decimal?>(source, pageNumber, pageSize, x => x.PricePerMonth);
        }

    }
}