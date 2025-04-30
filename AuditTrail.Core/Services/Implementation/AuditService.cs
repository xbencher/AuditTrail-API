
using AuditTrail.Core.Domain;
using AuditTrail.Core.Enums;
using Infrastructure.Abstraction;

namespace AuditTrail.Core.Services.Implementation
{
    public class AuditService : IAuditService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task LogChangeAsync<T>(
            T before,
            T after,
            AuditAction action,
            string entityName,
            string userId)
            where T : class
        {
            var changes = GetChanges(before, after);

            if (changes.Count == 0 && action == AuditAction.Updated)
                return; // Skip if no changes in update

            var auditLog = new AuditLog
            {
                Action = action,
                EntityName = entityName,
                UserId = userId,
                Timestamp = DateTime.UtcNow,
                ChangesJson = Newtonsoft.Json.JsonConvert.SerializeObject(changes)
            };

            var repo = _unitOfWork.Repository<AuditLog>();
            await repo.AddAsync(auditLog);
            await _unitOfWork.SaveChangesAsync();
        }

        public Dictionary<string, object> GetChanges<T>(T before, T after)
            where T : class
        {
            var changes = new Dictionary<string, object>();

            if (before == null || after == null)
                return changes;

            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var beforeValue = prop.GetValue(before);
                var afterValue = prop.GetValue(after);

                if (beforeValue?.ToString() != afterValue?.ToString())
                {
                    changes[prop.Name] = new
                    {
                        Old = beforeValue,
                        New = afterValue
                    };
                }
            }

            return changes;
        }
    }
}
