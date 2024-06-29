using System;
using System.Net;
using System.Threading.Tasks;
using LaraSharp_Framework.Html;
using LaraSharp_Framework.Html.Javascript;
using LaraSharp_Framework.Log;
using LaraSharp_Framework.Server;

namespace LaraSharp_Framework.Settings
{
   public class Instance
    {
        public static HttpServer actionHandler;
        public static Settings settings = new Settings();
        public static DomEditor domdocument;
        public static Debugger debugger;
        public static Routes Route;
        public static ApiRouter api;
        public static Http http;
        public static string Host {get; set;}
        public static string Port {get; set;}
        
        public void Initialize()
        {
            debugger = new Debugger();
            settings = new Settings();
            
            settings.Initializ();
            settings.SetSettings();
            
            Host = settings.GetSetting("HOST");
            Port = settings.GetSetting("PORT");
            
            Route = new Routes();
            actionHandler = new HttpServer();
            api = new ApiRouter();
            domdocument = new DomEditor();
        }
        
        
        public static void RegisterRoute(string route, HtmlBuilder view)
        {
            Routes.RegisterRoute(route, view);
        }
        
        public static void RegisterApiRoute(string route, Func<HttpListenerRequest, string> action)
        {
            api.AddApi(route, request => Task.FromResult(action(request)));
        }
        
       

        public static void StartUp()
        {
            http = new Http();
            Task.Run(() => actionHandler.StartServer());
            Task.Run(() => new DomEditor().EditDomListener());
            Task.Run(() => api.StartServerAsync());
            http.Start();
        }
        
    }
    
}