using Microsoft.Extensions.Logging;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Library.Middleware
{
    public class DepartmentValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DepartmentValidationMiddleware> _logger;

        public DepartmentValidationMiddleware(RequestDelegate next, ILogger<DepartmentValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public DepartmentValidationMiddleware()
        {

        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository,ICopyRepository copyRepository)
        {
            if (int.TryParse(context.Request.Query["copyId"], out int copyId) &&
                int.TryParse(context.Request.Headers["userId"], out int userId))
            {
                //var departmentType = userRepository.GetDepartmentTypeById(userId);
                //var itemGenre = copyRepository.GetGenreTypeById(copyId);

                //if (departmentType != itemGenre)
                //{
                //    context.Response.StatusCode = 403;
                //    await context.Response.WriteAsync("Forbidden.");
                //    return;
                //}

                await _next(context);
            }
                _logger.LogWarning("Department validation failed for copyId: {CopyID}", copyId);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Department validation failed");
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<DepartmentValidationMiddleware>();
        }
    }
}
