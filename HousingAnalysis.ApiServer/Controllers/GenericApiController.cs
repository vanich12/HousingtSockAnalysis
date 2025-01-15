using HousingAnalysis.ApiServer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HousingAnalysis.ApiServer.Models;

namespace HousingAnalysis.ApiServer.Controllers
{
    public abstract class GenericApiController<TEntity> : ControllerBase where TEntity : class
    {
        #region Поля и свойства

        private readonly IGenericRepository<TEntity> _rep;
        private readonly ILogger<TEntity> _logger;

        #endregion

        [HttpGet]
        public virtual async Task<IActionResult> ListAll()
        {
            try
            {
                return this.Ok(await this._rep.Get());
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Error while getting Entities");
                return this.Exception();
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var form = await this._rep.FindById(id);
                return form is null ? this.NotFoundException() : this.Ok(form);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Error while getting Entity by Id");
                return this.Exception();
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity form)
        {
            try
            {
                await this._rep.Create(form);
                return this.StatusCode(StatusCodes.Status201Created, form);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Error while creating new Entity");
                return this.Exception();
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(Guid id, TEntity form)
        {
            try
            {
                var result = await this._rep.Update(id, form);
                return result is null ? this.NotFoundException() : this.Ok(form);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Error while updating Entity");
                return this.Exception();
            }
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete(TEntity form)
        {
            try
            {
                await this._rep.Remove(form);
                return this.StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Error while deleting Entity");
                return this.Exception();
            }
        }

        protected IActionResult Exception()
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                this.GetDefaultResponse());
        }

        protected IActionResult NotFoundException()
        {
            return this.NotFound(this.GetDefaultResponse());
        }

        private DefaultResponse GetDefaultResponse()
        {
            return new DefaultResponse
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            };
        }
    }
}