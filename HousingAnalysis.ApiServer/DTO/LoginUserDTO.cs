using System.ComponentModel.DataAnnotations;

namespace HousingAnalysis.ApiServer.DTO
{
    public record LoginUserDTO(
        [Required] string Email,
        [Required] string Password);

}
