
using AuditTrail.Core.Enums;

namespace AuditTrail.Core.Models
{
    public class AuditRequestModel
    {
        public object Before { get; set; }
        public object After { get; set; }
        public string EntityName { get; set; }
        public string UserId { get; set; }
        public AuditAction Action { get; set; }
    }
}
