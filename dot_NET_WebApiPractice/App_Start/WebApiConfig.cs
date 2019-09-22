using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace dot_NET_WebApiPractice
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.AddApiVersioning(cfg=>
            {
                cfg.DefaultApiVersion = new ApiVersion(2,0);
                cfg.AssumeDefaultVersionWhenUnspecified=true;
                cfg.ReportApiVersions = true;
            }
            );

            /*
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            */
        }
    }
}
