using House.Common.EntityModels.PostgreSQL.Packt.Shared;
using HousingAnalysis.ApiServer.Extension;
using HousingAnalysis.ApiServer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Packt.Shared;

namespace HousingAnalysis.ApiServer.Repository
{
    public class HouseRepository : GenericRepository<Offer>,IHousePropertyRepository
    {
        #region Поля и свойства

        private readonly HouseDbContext _context;

        #endregion

        public HouseRepository(HouseDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<PagedPage<Offer>> GetHousesByPage(int pageNumber, int pageSize)
        {
            IQueryable<Offer> source = this._context.Offers;
            return await PagedPage<Offer>.ToPagedPage<decimal?>(source, pageNumber, pageSize);
        }

    }
}