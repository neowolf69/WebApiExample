using Learning.Web.Filters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Learning.Web.Services;

namespace Learning.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Services.Replace(typeof(IHttpControllerSelector), new LearningControllerSelector((config)));

            config.Routes.MapHttpRoute(
                name: "Courses",
                routeTemplate: "api/courses/{id}",
                defaults: new { controller = "courses", id = RouteParameter.Optional }
            );

            
            config.Routes.MapHttpRoute(
                name: "Enrollments",
                routeTemplate: "api/courses/{courseId}/students/{userName}",
                defaults: new { controller = "Enrollments", userName = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Students",
                routeTemplate: "api/students/{userName}",
                defaults: new { controller = "students", userName = RouteParameter.Optional }
            );

            /***Uncomment the Routes Students & Students2 to allow versioning withing URLs***/
            //config.Routes.MapHttpRoute(
            //    name: "Students",
            //    routeTemplate: "api/v1/students/{userName}",
            //    defaults: new { controller = "students", userName = RouteParameter.Optional }
            //    );

            //config.Routes.MapHttpRoute(
            //                name: "Students2",
            //                routeTemplate: "api/v2/students/{userName}",
            //                defaults: new { controller = "studentsV2", userName = RouteParameter.Optional }
            //                );



            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling= Newtonsoft.Json.ReferenceLoopHandling.Ignore; 

            //To force https to all methods of the webapi
            //config.Filters.Add(new ForceHttpsAttribute());

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            
        }
    }
}
