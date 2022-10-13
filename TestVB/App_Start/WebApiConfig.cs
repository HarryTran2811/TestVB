using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Extensions.Logging;
using TestVB.Data;
using System.Configuration;

public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Web API configuration and services

        var services = new ServiceCollection();

        // register logging 
        services.AddSingleton(typeof(ILogger[]), typeof(Logger[]));
        services.AddLogging(builder =>
        {
            builder.AddNLog();
        });
        ConfigureServices(services);

        //// Register the ExceptionLogger
        //// services.AddSingleton(Of IExceptionLogger, GlobalExceptionLogger)()

        //// Set services Resolver
        //config.DependencyResolver = new ServiceResolver(services.BuildServiceProvider());

        //// Enable jwt authorization
        //config.Filters.Add(new JwtAuthenticationAttribute());

        // Web API routes
        config.MapHttpAttributeRoutes();

        config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional }
);

        // ' response in json format
        var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t =>
        {
            return t.MediaType == "application/xml";
        });
        config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

        // Config json serializer
        var json = config.Formatters.JsonFormatter;
        // .Add(New JsonMediaTypeFormatter())
        json.SerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        };
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        // Register all controllers which existed in the assembly
//        services.AddControllersAsServices(typeof(WebApiConfig).Assembly.GetExportedTypes()
//                .Where(t =>
//                {
//                    return !t.IsAbstract && !t.IsGenericTypeDefinition;
//                })
//                .Where(t =>
//                {
//                    return typeof(IHttpController).IsAssignableFrom(t) || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase);
//                }
//));

        // Register DB context
        services.AddScoped<DbContext, AppDbContext>();
        //// Register unit of works
        //services.AddScoped<CustomerUnitOfWork>();
    }
}
