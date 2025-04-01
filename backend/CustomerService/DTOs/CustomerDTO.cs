using System.ComponentModel.DataAnnotations;

namespace CustomerService.DTOs
{
    public class CustomerDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }
    }
}