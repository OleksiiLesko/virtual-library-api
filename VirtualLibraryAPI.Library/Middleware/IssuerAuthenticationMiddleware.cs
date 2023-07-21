using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Configuration;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;
using UserType = VirtualLibraryAPI.Common.UserType;

namespace VirtualLibraryAPI.Library.Middleware
{
    public class IssuerAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IssuerAuthenticationMiddleware> _logger;

        public IssuerAuthenticationMiddleware(RequestDelegate next, ILogger<IssuerAuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public IssuerAuthenticationMiddleware()
        {

        }

        public async Task Invoke(HttpContext context, IUserRepository repository)
        {
            if (int.TryParse(context.Request.Headers["issuerId"], out int issuerId))
            {
                var userType = repository.GetUserTypeById(issuerId);

                if (userType == UserType.Administrator || userType == UserType.Manager)
                {
                    await _next(context);
                    return;
                }
                _logger.LogWarning("Authentication failed for adminId: {IssuerId}", issuerId);
            }
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authentication failed");
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<IssuerAuthenticationMiddleware>();
        }
    }
}