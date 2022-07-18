using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.Hosting;
using Replikit.Core.Hosting;
using Replikit.Examples.EntityFrameworkCore;

var builder = Host.CreateDefaultBuilder();

builder.AddModule<EfCoreExampleModule>();

builder.AddDevelopmentUserSecrets<Program>();

var host = builder.Build();

host.Run();
