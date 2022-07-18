using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.Hosting;
using Replikit.Core.Hosting;
using Replikit.Examples;

var builder = Host.CreateDefaultBuilder();

builder.AddModule<MainModule>();

builder.AddDevelopmentUserSecrets<Program>();

var host = builder.Build();

host.Run();
