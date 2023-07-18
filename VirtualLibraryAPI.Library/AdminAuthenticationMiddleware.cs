using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Configuration;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;
using UserType = VirtualLibraryAPI.Common.UserType;

namespace VirtualLibraryAPI.Library
{
    public class AdminAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AdminAuthenticationMiddleware> _logger;

        public AdminAuthenticationMiddleware(RequestDelegate next,ILogger<AdminAuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public AdminAuthenticationMiddleware()
        {
            
        }

        public async Task Invoke(HttpContext context, IUserRepository repository)
        {
            if (int.TryParse(context.Request.Headers["adminId"], out int adminId))
            {
                var userType = repository.GetUserTypeById(adminId);

                if (userType == UserType.Administrator)
                {
                    await _next(context);
                    return;
                }
                _logger.LogWarning("Authentication failed for adminId: {AdminId}", adminId);
            }
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authentication failed");
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<AdminAuthenticationMiddleware>();
        }
    }
}