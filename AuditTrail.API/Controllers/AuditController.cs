using AuditTrail.Core.Enums;
using AuditTrail.Core.Models;
using AuditTrail.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace AuditTrail.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditController : ControllerBase
    {
        private readonly IAuditService _auditService;

        public AuditController(IAuditService auditService)
        {
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
        }

        [HttpPost("log")]
        public async Task<IActionResult> LogAudit([FromBody] RequestWrapper request)
        {
            if (!Enum.TryParse<AuditAction>(request.Request.Action, out var action))
                return BadRequest("Invalid AuditAction.");

            var changes = _auditService.GetChanges(request.Request.Before, request.Request.After);
            // Log the change
            await _auditService.LogChangeAsync(
                request.Request.Before,
                request.Request.After,
                action,
                request.Request.EntityName,
                request.Request.UserId
            );

            return Ok(new { message = "Audit log saved.", changes});
        }
    }
}
