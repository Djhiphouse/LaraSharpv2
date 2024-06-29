using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LaraSharp_Framework.Settings;

namespace LaraSharp_Framework.Html.Javascript
{
    public class DomEditor
    {
        private static Queue<string> commandQueue = new Queue<string>();
        private static HttpListener listener = new HttpListener();

        public DomEditor()
        {
            listener.Prefixes.Add("http://" + Instance.Host + ":8001/");
        }

        public async Task EditDomListener()
        {
            listener.Start();
            Instance.debugger.DebugInfo($@"Server started on http://{Instance.Host}:8001", "DOM EDIT LISTENER");
            while (true)
            {
                var context = await listener.GetContextAsync();
                var response = context.Response;
                response.Headers.Add("Access-Control-Allow-Origin", "*");  // Handle CORS here

                string command = commandQueue.Count > 0 ? commandQueue.Dequeue() : "Queue is empty";

                byte[] buffer = Encoding.UTF8.GetBytes(command);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
        }


        public void AddToQueue(string elementById, string value)
        {
            string command = $"document.getElementById('{elementById}').innerText = '{value}';";
            commandQueue.Enqueue(command);
            Instance.debugger.DebugInfo($"Edit Element -> " + elementById,"LIVE REFRESH");
        }
        
        public void AddJavascript(string script)
        {
            commandQueue.Enqueue(script);
            Instance.debugger.DebugInfo($"Add Javascript -> " + script,"LIVE REFRESH");
        }
    }
}