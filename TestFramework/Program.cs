

using LaraSharp_Framework;
using LaraSharp_Framework.Settings;
using LaraSharp_Framework.Views;

namespace TestFramework
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            LaraSharp laraSharp = new LaraSharp();
            ErrorView.Initialize();
            
            Instance.RegisterRoute("/error", ErrorView.HtmlBuilder);
            laraSharp.StartUp();
        }
    }
}