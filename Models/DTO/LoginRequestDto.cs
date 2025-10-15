using System.ComponentModel.DataAnnotations;

namespace NGWALKSAPI.Models.DTO
{
    public class LoginResponsetDto
    {
        [Required]

        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
