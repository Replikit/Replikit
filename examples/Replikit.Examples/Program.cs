using Kantaiko.Modularity;
using Microsoft.Extensions.Hosting;
using Replikit.Adapters.Telegram;
using Replikit.Core.Hosting;
using Replikit.Examples;

var builder = Host.CreateDefaultBuilder();

builder.ConfigureReplikit(replikit =>
{
    replikit.Services.AddModule<ExampleModule>();

    replikit.ConfigureAdapters(adapters => adapters.AddTelegram());
});

builder.ConfigureDevelopmentUserSecrets<Program>();

var host = builder.Build();

host.Run();
