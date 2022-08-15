using Kantaiko.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Replikit.Adapters.Telegram;
using Replikit.Core.Hosting;
using Replikit.Examples;
using Replikit.Integrations.MongoDB;

var builder = Host.CreateDefaultBuilder();

builder.ConfigureReplikit(replikit =>
{
    replikit.Services.AddModule<ExampleModule>();

    replikit.Services.AddReplikitMongoDBIntegration(mongodb =>
    {
        var connectionString = replikit.Context.Configuration.GetConnectionString("Default");

        mongodb.AddDatabase(connectionString);
        mongodb.AddDefaults();
    });

    replikit.ConfigureAdapters(adapters => adapters.AddTelegram());
});

builder.ConfigureDevelopmentUserSecrets<Program>();

var host = builder.Build();

host.Run();
