using HousingAnalysis.ApiServer.Extension;
using HousingAnalysis.ApiServer.Repository;
using HousingAnalysis.ApiServer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Packt.Shared;

namespace HousingAnalysis.ApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class HouseStockController : GenericApiController<Property>
    {
        private readonly ILogger<Property> _logger;
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

        public HouseStockController(ILogger<Property> logger, IHousePropertyRepository houseRepository,
            IGenericRepository<Property> repository) : base(logger, repository)
        {
            this._houseRep = houseRepository;
            this._logger = logger;
        }
    }
}