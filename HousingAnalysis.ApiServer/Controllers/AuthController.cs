using Asp.Versioning;
using House.Common.EntityModels.PostgreSQL.Packt.Shared;
using HousingAnalysis.ApiServer.DTO;
using HousingAnalysis.ApiServer.Repository.Interfaces;
using HousingAnalysis.ApiServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HousingAnalysis.ApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : GenericApiController<User>
    {
        private readonly ILogger<User> _logger;
        private readonly IUserService _userService;

        public AuthController(ILogger<User> logger, IGenericRepository<User> repository, IUserService userService) :
            base(logger, repository)
        {
            this._logger = logger;
            this._userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDTO userDTO)
        {
            if (userDTO == null)
                return this.StatusCode(StatusCodes.Status400BadRequest, userDTO);
            try
            {
                await this._userService.Register(userDTO.Name, userDTO.Email, userDTO.Password);
                return this.StatusCode(StatusCodes.Status201Created, userDTO);
            }
            catch (Exception e)
            {
                this._logger.LogError($"Не удалось зарегестрировать пользователся с ошибкой /n {e}");
                return this.Exception();
            }
        }

        [HttpPost("logIn")]
        public async Task<IActionResult> LogIn([FromBody] LoginUserDTO userDTO)
        {
            try
            {
                var token = await this._userService.LogIn(userDTO.Email, userDTO.Password);
                HttpContext.Response.Cookies.Append("tasty-cookie", token);
                return Ok(token);
            }
            catch (Exception e)
            {
                this._logger.LogError($"Не удалось аутентифицировать пользователся с ошибкой /n {e}");
                return this.Exception();
            }
        }
    }
}