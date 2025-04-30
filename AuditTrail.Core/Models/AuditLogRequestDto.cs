using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace AuditTrail.Core.Models
{
    public class RequestWrapper
    {
        [Required]
        public AuditLogRequestDto Request { get; set; }
    }

    public class AuditLogRequestDto
    {
        [Required]
        public string Action { get; set; }

        [Required]
        public string EntityName { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public Person Before { get; set; }

        [Required]
        public Person After { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Occupation { get; set; }
        public bool IsActive { get; set; }
    }
}
