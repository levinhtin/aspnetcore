using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Middleware
{
    public static class AuthorizationAppBuilderExtensions
    {
        public static IApplicationBuilder UseAuthorization(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
