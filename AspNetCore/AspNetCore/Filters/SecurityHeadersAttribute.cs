using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Meyer.Common.AspNetCore.Filters
{
    public class SecurityHeadersAttribute : ActionFilterAttribute
    {
        public bool CanCache { get; }

        public SecurityHeadersAttribute(bool canCache)
        {
            this.CanCache = canCache;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            IHeaderDictionary headers = context.HttpContext.Response.Headers;

            if (!headers.ContainsKey("X-Content-Type-Options"))
                headers.Add("X-Content-Type-Options", "nosniff");

            if (!headers.ContainsKey("X-Frame-Options"))
                headers.Add("X-Frame-Options", "SAMEORIGIN");

            if (!headers.ContainsKey("Content-Security-Policy"))
                headers.Add("Content-Security-Policy", "default-src 'self'; object-src 'none'; frame-ancestors 'none'; sandbox allow-forms allow-same-origin allow-scripts; base-uri 'self';");

            if (!headers.ContainsKey("X-Content-Security-Policy"))
                headers.Add("X-Content-Security-Policy", "default-src 'self'; object-src 'none'; frame-ancestors 'none'; sandbox allow-forms allow-same-origin allow-scripts; base-uri 'self';");

            if (!headers.ContainsKey("Content-Disposition"))
                headers.Add("Content-Disposition", "inline");

            if (!this.CanCache && !headers.ContainsKey("Cache-Control"))
                headers.Add("Cache-Control", "no-store");
        }
    }
}