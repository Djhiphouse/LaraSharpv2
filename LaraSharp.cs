using LaraSharp_Framework.Settings;
using LaraSharp_Framework.Views;
using System.Threading;

namespace LaraSharp_Framework
{
  public class LaraSharp
  {
    private Instance laraSharp = new Instance();

    public void StartUp()
    {
      ErrorView.Initialize();
      WelcomeView.Initialize();
      Instance.RegisterRoute("/error", ErrorView.HtmlBuilder);
      Instance.RegisterRoute("/welcome", WelcomeView.HtmlBuilder);
      Instance.StartUp();
      Thread.Sleep(-1);
    }

    public void Initialize() => this.laraSharp.Initialize();
  }
}
