using System;
using System.Net;
using System.Threading.Tasks;
using LaraSharp_Framework.Settings;

namespace LaraSharp_Framework.Server
{
   public class Http
    {
        public string host { get; set; }
        public int port { get; set; }
        public static string view { get; set; }
        
        private static HttpListener listener;
        public Http()
        {
            this.host = Instance.Host;
            this.port = Int32.Parse(Instance.Port);
            
            listener = new HttpListener();
            listener.Prefixes.Add($"http://{this.host}:{this.port}/");

            Instance.debugger.DebugSuccess($@"Server is running on {this.host}:{this.port}", "HTTP SERVER");
            Log.Logger.LogMessage("Route Register -> Success");
        }
        
        public void Start()
        {
            listener.Start();
            Instance.debugger.DebugSuccess($@"Listening....", "HTTP LISTENING");
            Log.Logger.LogMessage("Http Status -> Started");
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();
            listener.Close();
        }

        private static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
            {
                HttpListenerContext ctx = await listener.GetContextAsync();

                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;
                
               
                string route = String.Empty;
                
                try
                {
                     route = req.Url.AbsolutePath.ToString().Split('/')[1];
                }
                catch (Exception e)
                {
                    Instance.debugger.DebugError("Route not found", "NOT FOUND");
                    Log.Logger.LogMessage("Route not found -> " + req.RemoteEndPoint.ToString());
                    Instance.debugger.DebugError("Redirecting to 404", "NOT FOUND");
                    return;
                }
               
                
                if (req.HttpMethod == "GET")
                {
                    Instance.debugger.DebugInfo("Request GET -> Receive from " + req.RemoteEndPoint.ToString() + " Route -> " + route, "HTTP GET");
                    Log.Logger.LogMessage("Request GET -> Receive from " + req.RemoteEndPoint.ToString() + " Route -> " + route);
                    view = Routes.GetRoute(route);
                    Instance.debugger.DebugInfo("View -> " + route, "HTTP GET");
                    
                    Instance.debugger.DebugInfo("Add Route to Cache -> " + route, "HTTP GET");
                    await Instance.Route.AddCachedRoute(route);
                }
                else if (req.HttpMethod == "POST")
                {
                    Instance.debugger.DebugInfo("Request POST -> Recive from " + req.RemoteEndPoint.ToString() + " Route -> " + route, "HTTP POST");
                     Log.Logger.LogMessage("Request POST -> Recive from " + req.RemoteEndPoint.ToString() + " Route -> " + route);
                     view = Routes.GetRoute(route);
                }
                
               await Response(ctx, view);
            }
        }
        
        public static async Task Response(HttpListenerContext context, string view)
        {
            HttpListenerResponse resp = context.Response;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(view);
                
            resp.ContentType = "text/html";
            resp.ContentEncoding = System.Text.Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;

            await resp.OutputStream.WriteAsync(data, 0, data.Length);
            resp.Close();
        }
    }
}