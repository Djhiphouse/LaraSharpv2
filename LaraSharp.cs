using System.Threading;
using LaraSharp_Framework.Settings;
using LaraSharp_Framework.Views;

namespace LaraSharp_Framework
{
    public class LaraSharp
    {
        public void StartUp()
        {
            Instance laraSharp = new Instance();
            laraSharp.Initialize();
            
            ErrorView.Initialize();
            WelcomeView.Initialize();
            
            Instance.RegisterRoute("/error", ErrorView.HtmlBuilder);
            Instance.RegisterRoute("/welcome", WelcomeView.HtmlBuilder);
            
            Instance.StartUp();
            Thread.Sleep(-1);
        }
        
    }
}