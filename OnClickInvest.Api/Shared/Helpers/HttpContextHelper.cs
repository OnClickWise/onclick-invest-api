using System.Security.Claims;

namespace OnClickInvest.Api.Shared.Helpers
{
    public static class HttpContextHelper
    {
        public static Guid? GetTenantId(HttpContext context)
        {
            if (context.Items.TryGetValue("TenantId", out var value))
            {
                if (Guid.TryParse(value?.ToString(), out var guid))
                    return guid;
            }

            return null;
        }

        public static Guid? GetUserId(HttpContext context)
        {
            var claim = context.User.FindFirst("userId")?.Value;

            if (Guid.TryParse(claim, out var guid))
                return guid;

            return null;
        }
    }
}
