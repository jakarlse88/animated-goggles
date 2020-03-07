using System.ComponentModel.DataAnnotations;

namespace Demelain.Client.Models.InputModels
{
    public class ContactFormInputModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }
        
        [Required]
        public string Subject { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string Email { get; set; }
        
        [Required]
        public string Message { get; set; }
    }
}