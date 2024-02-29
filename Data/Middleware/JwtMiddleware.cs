using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MvcMovie.Data.Auth.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWT_TOKEN");

            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Add("Authorization", $"Bearer {token}");
            }

            await _next(context);
        }
    }
}
