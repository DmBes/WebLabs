using System;
using ASP.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ASP.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T item)
        {
            var serializedItem = JsonConvert.SerializeObject(item); 
            session.SetString(key, serializedItem);

        }

        public static T Get<T>(this ISession session, string key)
        {
            var item = session.GetString(key); 
            return item == null 
                ? Activator.CreateInstance<T>() // default(T)
                : JsonConvert.DeserializeObject<T>(item);
        }



    }


    public static class AppExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogMiddleware>();
        }
    }
}
