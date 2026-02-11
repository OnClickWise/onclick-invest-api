using System.Security.Claims;

namespace OnClickInvest.Api.Shared.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? tenantId = null;

            if (context.User.Identity?.IsAuthenticated == true)
            {
                tenantId = context.User.FindFirst("tenantId")?.Value;
            }

            if (string.IsNullOrEmpty(tenantId))
            {
                tenantId = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(tenantId))
            {
                context.Items["TenantId"] = tenantId;
            }

            await _next(context);
        }
    }
}
