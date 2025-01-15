using HousingAnalysis.ApiServer.Extension;
using HousingAnalysis.ApiServer.Repository;
using Microsoft.AspNetCore.Mvc;
using Packt.Shared;

namespace HousingAnalysis.ApiServer.Controllers
{
    public class HouseStockController : GenericApiController<Property>
    {
        private readonly ILogger<Property> _logger;
        private readonly HouseRepository _houseRep;

        [HttpGet("paged")]
        public async Task<IActionResult> ListAllPaged([FromQuery] Pageable page)
        {
            try
            {
                var items = await this._houseRep.GetHousesByPage(page.PageNumber, page.PageSize);
                return this.Ok(items);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Error while getting Entities");
                return this.Exception();
            }
        }
        public HouseStockController(ILogger<Property> logger, HouseRepository repository) : base(logger, repository)
        {
            this._houseRep = repository;
            this._logger = logger;
        }
    }
}