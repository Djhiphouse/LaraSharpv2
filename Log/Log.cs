using System.IO;

namespace LaraSharp_Framework.Log
{
    public class Logger
    {
        public static void LogMessage(string log)
        {
            var logDirectory = Path.GetDirectoryName(@"Log/logs.log");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            
            using (StreamWriter sw = File.AppendText(@"Log/logs.log"))
            {
                sw.WriteLine(log +  " : " + System.DateTime.Now);
            }
        }
    }
}