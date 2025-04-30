

using AuditTrail.Core.Enums;

namespace AuditTrail.Core.Services
{
    public interface IAuditService
    {
        Dictionary<string, object> GetChanges<T>(T before, T after) where T : class;
        Task LogChangeAsync<T>(T before, T after, AuditAction action, string entityName, string userId) where T : class;
    }
}
