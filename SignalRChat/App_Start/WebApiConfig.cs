using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using SignalRChat.Models;


namespace SignalRChat
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Commenting these out because I *only* want OData, and 
            // leaving them as-is breaks SignalR's mapping

            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Confession>("ConfessionsOData");
            builder.EntitySet<HashTag>("HashTags");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
