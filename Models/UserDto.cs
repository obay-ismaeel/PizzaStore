using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
