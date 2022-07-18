using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.Hosting;
using Replikit.Core.Hosting;
using Replikit.Examples.MongoDB;

var builder = Host.CreateDefaultBuilder();

builder.AddModule<MongoDBExampleModule>();

builder.AddDevelopmentUserSecrets<Program>();

var host = builder.Build();

host.Run();
