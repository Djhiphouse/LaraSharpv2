using System;
using System.Collections.Generic;
using System.IO;

namespace LaraSharp_Framework.Settings
{
    public class Settings
    {
        public static  Dictionary<string, string> Setting = new Dictionary<string, string>();

        public void SetSettings()
        {
            foreach (var setting in File.ReadAllLines("Settings/env.conf"))
            {
                if (string.IsNullOrWhiteSpace(setting))
                    continue;
        
                Console.WriteLine("Setting -> " + setting);
                string[] currentSetting = setting.Split('=');
                if (currentSetting.Length == 2) // Ensure there are exactly two parts: key and value.
                { 
                    Setting[currentSetting[0].Trim()] = currentSetting[1].Trim(); // Use Trim to remove any leading/trailing whitespace.
                }
               
            }
        }

        
        public string GetSetting(string setting)
        {
            if (Setting.ContainsKey(setting))
            {
                return Setting[setting];
            }
            Log.Logger.LogMessage("Setting -> " + setting + " not found");
            return null;
        }

        public void Initializ()
        {
            if (Directory.Exists("Settings"))
            {
                Console.WriteLine("Settings -> Found");
                if (!File.Exists("Settings/env.conf"))
                {
                    Console.WriteLine("Settings -> env.conf not found");
                    using (File.Create("Settings/env.conf")) { }
                    File.WriteAllText("Settings/env.conf", @"
                HOST=127.0.0.1
                PORT=3000

                DB_HOST=localhost
                DB_DATABASE=DB_DATABASE
                DB_USER=DB_USER
                DB_PASS=DB_PASS
            ".Replace(" ", ""));
                }
            }
            else
            {
                Console.WriteLine("Settings -> Not Found");
                Directory.CreateDirectory("Settings");
                using (File.Create("Settings/env.conf")) { }
                File.WriteAllText("Settings/env.conf", @"
                HOST=127.0.0.1
                PORT=3006

                DB_HOST=localhost
                DB_DATABASE=DB_DATABASE
                DB_USER=DB_USER
                DB_PASS=DB_PASS
            ".Replace(" ", ""));
            }

            if (Directory.Exists("Log"))
            {
                Console.WriteLine("Log -> Found");
                if (!File.Exists("Log/logs.log"))
                {
                    Console.WriteLine("Log -> logs.log not found");
                    using (File.Create("Log/logs.log")) { }
                }
            }
            else
            {
                Console.WriteLine("Log -> Not Found");
                Directory.CreateDirectory("Log");
                using (File.Create("Log/logs.log")) { }
            }

            if (!Directory.Exists("Icons"))
            {
                Console.WriteLine("Icons -> Not Found");
                Directory.CreateDirectory("Icons");
            }

            if (Directory.Exists("Icons") && Directory.Exists("Settings") && Directory.Exists("Log"))
            {
                Console.WriteLine("Initialization -> Success");
                Log.Logger.LogMessage("Initialization -> Success");
            }
            else
            {
                Environment.Exit(0);
            }
        }

    }
}