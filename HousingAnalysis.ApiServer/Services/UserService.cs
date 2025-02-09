using House.Common.EntityModels.PostgreSQL.Packt.Shared;
using HousingAnalysis.ApiServer.DTO;
using HousingAnalysis.ApiServer.Extension.Interfaces;
using HousingAnalysis.ApiServer.Repository.Interfaces;
using HousingAnalysis.ApiServer.Services.Interfaces;

using HousingAnalysis.ApiServer.Utilities;

namespace HousingAnalysis.ApiServer.Services
{
    public class UserService:IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public async Task Register(string name, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);
            var id = Guid.NewGuid();
            var user = new User()
            {
                Id = id.ToString(),
                UserName = name, 
                Email = email,
                PasswordHash = hashedPassword
            };

            await _userRepository.Create(user);
        }

        public async Task<string> LogIn(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (!result)
            {
                throw new Exception("Invalid Password");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            this._passwordHasher = passwordHasher;
            this._userRepository = userRepository;
            this._jwtProvider = jwtProvider;
        }
    }
}