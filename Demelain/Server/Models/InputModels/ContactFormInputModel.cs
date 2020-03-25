using System.ComponentModel.DataAnnotations;

namespace Demelain.Server.Models.InputModels
{
    public class ContactFormInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(2000)]
        public string Message { get; set; }
    }
}