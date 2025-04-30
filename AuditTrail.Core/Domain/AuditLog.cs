using AuditTrail.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail.Core.Domain
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; }
        public string UserId { get; set; }
        public AuditAction Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string ChangesJson { get; set; }
    }
}
