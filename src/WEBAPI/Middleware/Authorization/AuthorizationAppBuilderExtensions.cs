using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Middleware.Authentication;

namespace Microsoft.AspNetCore.Builder
{
    public static class AuthorizationAppBuilderExtensions
    {
        public static IApplicationBuilder UseAuthorization(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}