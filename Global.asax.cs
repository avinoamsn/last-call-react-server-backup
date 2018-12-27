using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace LastCallServer
{
    public class Global : HttpApplication
    {
        public static bool DebugMode = true;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Ensure the only thing we send back is JSON
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // Allow form data 
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));
            // Override for bug in JSON handling
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // If we're in debug mode, send back formatted JSON.
        public static Newtonsoft.Json.JsonSerializerSettings JSONSettings()
        {
            if (DebugMode)
            {
                Newtonsoft.Json.JsonSerializerSettings serializer = new Newtonsoft.Json.JsonSerializerSettings();
                serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializer.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                return serializer;
            }

            return null;
        }
    }
}