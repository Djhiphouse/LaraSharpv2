using LaraSharp_Framework.Html;
using LaraSharp_Framework.Settings;

namespace LaraSharp_Framework.Views
{
    public class ErrorView
    {
        public static HtmlBuilder HtmlBuilder;
        
        public static void GoHome()
        {
            Instance.Route.Redirect("/welcome");
        }
        
        public static void Initialize()
        {
            HtmlBuilder = new HtmlBuilder();
            HtmlBuilder
                .Build()
                    .AddHead()
                    .Page()
                        .Div("w-full h-screen font-bold flex flex-col justify-center items-center bg-black")
                            .Div("animate-bounce")
                            .AddImage("https://github.com/Djhiphouse/LaraSharp/blob/main/Project/LaraSharp/Icons/error.png?raw=true", "error", 24, 24)
                            .EndDiv()
                            
                            .AddText("Route no found", "error", 24)
                            .AddActionButton("Go Home", GoHome)
                            .EndDiv()
                        .EndCenter()
                    .EndPage()
                .GetHTML();

        }
    }
}