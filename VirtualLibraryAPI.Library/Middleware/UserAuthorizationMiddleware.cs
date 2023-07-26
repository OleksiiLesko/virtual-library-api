using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Library.Middleware
{
    public class UserAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserAuthorizationMiddleware> _logger;

        public UserAuthorizationMiddleware(RequestDelegate next, ILogger<UserAuthorizationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public UserAuthorizationMiddleware()
        {

        }
        public async Task Invoke(HttpContext context, IUserRepository repository)
        {
            if (int.TryParse(context.Request.Headers["userid"], out int userid))
            {
                var userType = repository.GetUserTypeById(userid);

                var requestUserType = context.Request.Query["userType"];

                //if (userType == UserType.Administrator && requestUserType != UserType.Client.ToString())
                //{
                //    _logger.LogWarning("User {UserId} attempted to add a non-client user as an Administrator.", userid);
                //    context.Response.StatusCode = 403;
                //    await context.Response.WriteAsync("Forbidden. Only clients can be added by administrators.");
                //    return;
                //}

                if (userType == UserType.Manager && requestUserType == UserType.Manager.ToString())
                {
                    _logger.LogWarning("User {UserId} attempted to add a non-administrator user as a Manager.", userid);
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Forbidden. Only administrators can be added by managers.");
                    return;
                }

                //if (userType == UserType.Client)
                //{
                //    _logger.LogWarning("User {UserId} attempted to add a user as a Client. Clients can't add users.", userid);
                //    context.Response.StatusCode = 403;
                //    await context.Response.WriteAsync("Forbidden. Clients can't be added by clients.");
                //    return;
                //}

                await _next(context);
                return;
            }

            _logger.LogWarning("User authorization failed for adminId: {AdminId}", userid);
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("User authorization failed");
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<UserAuthorizationMiddleware>();
        }
    }
}
