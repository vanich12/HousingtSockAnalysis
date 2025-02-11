using HousingAnalysis.ApiServer.Extension;
using HousingAnalysis.ApiServer.Repository;
using HousingAnalysis.ApiServer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using House.Common.EntityModels.PostgreSQL.Packt.Shared;
using Packt.Shared;

namespace HousingAnalysis.ApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class HouseStockController : GenericApiController<Offer>
    {
        private readonly ILogger<Offer> _logger;
        private readonly IHousePropertyRepository _houseRep;

        [HttpGet("paged")]
        public async Task<IActionResult> ListAllPaged([FromQuery] Pageable pageable)
        {
            try
            {
                var items = await this._houseRep.GetHousesByPage(pageable.PageNumber, pageable.PageSize);
                return this.Ok(items);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Error while getting Entities");
                return this.Exception();
            }
        }

        public HouseStockController(ILogger<Offer> logger, IHousePropertyRepository houseRepository,
            IGenericRepository<Offer> repository) : base(logger, repository)
        {
            this._houseRep = houseRepository;
            this._logger = logger;
        }
    }
}