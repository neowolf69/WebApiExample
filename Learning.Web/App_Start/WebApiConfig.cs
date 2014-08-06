using Learning.Web.Filters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Learning.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling= Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
            
            //To force https to all methods of the webapi
            //config.Filters.Add(new ForceHttpsAttribute());

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
            name: "Courses",
            routeTemplate: "api/courses/{id}",
            defaults: new { controller = "courses", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                    name: "Students",
                    routeTemplate: "api/students/{userName}",
                    defaults: new { controller = "students", userName = RouteParameter.Optional }
                    );

            config.Routes.MapHttpRoute(
                name: "Enrollments",
                routeTemplate: "api/courses/{courseId}/students/{userName}",
                defaults: new { controller = "Enrollments", userName = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
        }
    }
}
