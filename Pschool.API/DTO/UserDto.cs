using Pschool.API.Enum;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Pschool.API.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Role Role { get; set; }
        public int? ParentId { get; set; } = null;

    }
}
