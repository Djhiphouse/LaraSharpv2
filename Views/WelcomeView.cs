using LaraSharp_Framework.Html;
using LaraSharp_Framework.Settings;

namespace LaraSharp_Framework.Views
{
    public class WelcomeView
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
                        .Div("absolute top-0 left-0 text-white text-xl p-5")
                        .AddText("Made by Djhiphouse", "developer")
                        .EndDiv()
                
                        .Div("w-full h-screen font-bold flex flex-col justify-center items-center bg-black")
                            .Div("flex flex-row space-x-2 justify-center items-center animate-bounce")
                                .AddImage(@"https://cdn.discordapp.com/attachments/1165057088146386954/1255180248757174322/DALL_E-2024-06-25-17.14-removebg-preview.png?ex=667e2b73&is=667cd9f3&hm=30d5cbe7e17a56ab0617ee87686868f1cc4030e939290838f29603b8c9c7e1e2&", "LaraSharp", 24, 24)
                                .AddText("Welcome to LaraSharp", "framework_str", 24)
                                .AddTimedNotification("Welcome to LaraSharp", "test", 2000)
                            .EndDiv()
                        .EndDiv()
                    .EndPage()
                .GetHTML();

        }
    }
}