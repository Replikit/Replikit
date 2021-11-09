# Getting started

- Getting started
- [Controllers](controllers.md)
- [Messages](messages.md)
- [Routing](routing.md)
- [Adapter services](adapter-services.md)
- [Further reading](further-reading.md)

While being very flexible, Replikit provides a fairly wide choice of possible startup and integration scenarios, which
are described in detail in the next documents.

However, let's start with the simplest and most common patterns. The recommended way of getting started is just using
ready templates to create your projects.

They can be installed by this command:

```shell
dotnet new -i Replikit.Templates
```

After this stage, you will have two templates available: `replikitapp` and `replikitmodule`. Let's create a project
using first template:

```shell
dotnet new replikitapp -n MyProject
```

The created project will have a very minimalistic structure. One of the files named `MyProjectModule.cs` is the root
(main) module of the application. The comments placed in it make it clear what the methods of the module class are
responsible for:

```c#
public class MyProjectModule : ReplikitModule
{
    public override void ConfigureModules(IReplikitModuleCollection modules)
    {
        // Here you can add dependency modules
        // modules.Add<ScenesModule>();
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        // Here you can register services inside the DI container
        // services.AddSingleton<MyDependency>();
    }

    public override void ConfigureAdapters(IAdapterLoaderOptions options)
    {
        // Here you can register adapters
        // options.AddTelegram();
    }
}
```

The second file is the well-known `Program.cs`, which contains the entry point of the application. It just launches the
root module:

```c#
using MyProject;
using Replikit.Core.Hosting;

ReplikitHost.RunModule<MyProjectModule>();
```

There are also an `HelloController.cs` which contains an example controller:

```c#
public class HelloController : Controller
{
    [Pattern("hi")]
    public OutMessage Hello() => $"Hello, {Account.Username}!";
}
```

Looks very simple, doesn't it?

If we run the application now, it will not do anything useful. To give our bot life, we must install and configure at
least one adapter. Let it be a telegram adapter, install it and add it to the module:

```shell
dotnet add package Replikit.Adapters.Telegram
```

And uncomment the corresponding line in the `ConfigureAdapters` method:

```c#
public override void ConfigureAdapters(IAdapterLoaderOptions options)
{
    // Here you can register adapters
    options.AddTelegram();
}
```

You also should provide the access token (for Telegram, you can contact the [@BotFather](https://t.me/botfather) bot).
Edit a file named `appsettings.json` and place your token here:

```json
{
  "Replikit": {
    "Adapters": [
      {
        "Type": "tg",
        "Token": "<API_TOKEN>"
      }
    ]
  }
}
```

Start the application and verify that your bot is responding:

![](attachments/getting-started.png)

Read next: [Controllers](controllers.md)
