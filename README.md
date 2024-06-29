
# LaraSharp Framework

LaraSharp is a robust web framework for .NET developers that combines the power of C# with the flexibility of JavaScript, HTML, and Tailwind CSS to deliver dynamic web applications. Inspired by Laravel, LaraSharp offers a variety of built-in functionalities such as SQL and HTML builders, live DOM manipulation for real-time editing, inbuilt notifications, and the ability to trigger C# functions from web-based JavaScript buttons.

## Key Features

- **SQL Builder**: Easily build and execute SQL queries.
- **HTML Builder**: Construct HTML content programmatically.
- **Live DOM Manipulation**: Edit content directly on the web page in real-time.
- **Inbuilt Notifications**: Integrated system for user notifications.
- **JavaScript Interaction**: Trigger C# backend functions directly from the frontend.
- **Route Controller**: Manage web routes with a simple and intuitive API.

## Installation

To use LaraSharp in your project, first install the package via NuGet:

```
Install-Package LaraSharp
```

## Getting Started

### Configuration
Set up your project environment by configuring `Settings/env.conf` to specify the server URL and database credentials.

### Initialization
Implement LaraSharp in your project's main method:

```csharp
using LaraSharp_Framework;

class Program
{
    static void Main(string[] args)
    {
        var laraSharp = new LaraSharp_Framework.LaraSharp();
        Instance instance = new Instance();
        instance.Initialize();

        // Initialize Views Here
        ErrorView.Initialize();
        WelcomeView.Initialize();

        // Register Routes Here

        // Already Implemented
        // instance.RegisterRoute("/error", ErrorView.htmlBuilder);
        // instance.RegisterRoute("/welcome", WelcomeView.htmlBuilder);

        laraSharp.StartUp();
    }
}
```

## Implementing Views

Create a `Views` folder in your project, and add classes for each view. Here is a standard structure for a view:

```csharp
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
                      .AddText("Test", "test_str")
                      .AddActionButton("Go Home", GoHome)
                    .EndPage()
                .GetHTML();
        }
    }
}
```

## Implementing Models

Create a `Models` folder and add classes for each model. Here is how to define a model:

```csharp
using System.Threading.Tasks;
using LaraSharp_Framework.Models;

namespace LaraSharp_Framework.Models
{
    public class Logs
    {
        public static SqlBuilder builder;
        public static Sql sql;
        public static string table = "logs";

        public static void Initialize()
        {
            builder = new SqlBuilder(Instance.settings);
            sql = new Sql(builder.Database);
        }

        public static void Migrate()
        {
            var migrate = a new Migration(table)
                .Id() 
                .String("message") 
                .Timestamps();

            migrate.Build(Sql.Connection);
        }

        public static Task<string> GetLogs()
        {
            return sql.Select().From(table).ExecuteReadAsync();
        }
    }
}
```

## Register Routes

Register your views in the main method to link routes to view renderers:

```csharp
Instance.RegisterRoute("/error", ErrorView.HtmlBuilder);
```

## Conventions and Recommendations

- Always use `.AddHead()` when using backend functionalities such as redirect or live edit.
- Properly initialize all components in the main method before starting the application.

## Additional Resources

- [GitHub Repository](https://github.com/Djhiphouse/LaraSharpv2)
- [Tutorial Videos](https://www.youtube.com/channel/UCZSDmx0OzwltKUBT5H--bpg)

This documentation provides a quick guide on how to start using LaraSharp. For more detailed information, please refer to the full documentation included in the package.
