using Microsoft.AspNetCore.Identity;

namespace HousingAnalysis.ApiServer.DTO
{
    public record UserRequestDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}