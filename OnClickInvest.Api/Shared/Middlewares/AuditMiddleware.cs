using OnClickInvest.Api.Data;
using OnClickInvest.Api.Modules.Audit.Models;

namespace OnClickInvest.Api.Shared.Middlewares
{
    public class AuditMiddleware
    {
        private readonly RequestDelegate _next;

        public AuditMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext db)
        {
            await _next(context);

            if (!context.User.Identity?.IsAuthenticated ?? true)
                return;

            var tenantIdStr = context.Items["TenantId"]?.ToString();
            var userIdStr = context.User.FindFirst("userId")?.Value;

            if (!Guid.TryParse(tenantIdStr, out var tenantId))
                return;

            Guid? userId = null;
            if (Guid.TryParse(userIdStr, out var parsedUserId))
                userId = parsedUserId;

            var log = new AuditLog
            {
                TenantId = tenantId,
                UserId = userId,
                Action = $"{context.Request.Method} {context.Request.Path}",
                CreatedAt = DateTime.UtcNow
            };

            db.AuditLogs.Add(log);
            await db.SaveChangesAsync();
        }
    }
}
