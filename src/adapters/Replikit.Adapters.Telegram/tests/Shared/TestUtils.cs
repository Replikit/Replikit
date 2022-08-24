using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using NSubstitute;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Factory;
using Replikit.Adapters.Telegram.Tests.Shared.Serialization;
using Replikit.Core.Serialization;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;

namespace Replikit.Adapters.Telegram.Tests.Shared;

internal static class TestUtils
{
    public record struct TestAdapter(ITelegramBotClient Backend, TelegramAdapter Adapter);

    public static TestAdapter CreateAdapter()
    {
        var backend = Substitute.For<ITelegramBotClient>();

        var options = new TelegramAdapterOptions { Token = TestData.TestToken };
        var factoryContext = new AdapterFactoryContext();

        var adapter = new TelegramAdapter(factoryContext, backend, options);

        return new TestAdapter(backend, adapter);
    }

    public static async Task<TestAdapter> CreateAdapterAsync(CancellationToken cancellationToken = default)
    {
        var adapter = CreateAdapter();

        adapter.Backend
            .MakeRequestAsync(Arg.Any<GetMeRequest>(), CancellationToken.None)
            .Returns(TestData.TestBotUser);

        await adapter.Adapter.InitializeCoreAsync(cancellationToken);

        return adapter;
    }

    private static bool DeepCompare(object? a, object? b)
    {
        return JsonSerializer.Serialize(a) == JsonSerializer.Serialize(b);
    }

    private static void MockGenericRequest<TRequest, TResponse>(ITelegramBotClient backend,
        TRequest request, TResponse response) where TRequest : IRequest<TResponse>
    {
        backend.MakeRequestAsync(Arg.Is<TRequest>(i => DeepCompare(i, request)), CancellationToken.None)
            .Returns(response);
    }

    public static Task VerifyFeature(Func<IAdapter, Task<object>> action, UnderlyingRequest request,
        [CallerFilePath] string sourceFile = "")
    {
        // ReSharper disable once ExplicitCallerInfoArgument
        return VerifyFeature(action, new[] { request }, sourceFile);
    }

    public static async Task VerifyFeature(Func<IAdapter, Task<object>> action, IEnumerable<UnderlyingRequest> requests,
        [CallerFilePath] string sourceFile = "")
    {
        var (backend, adapter) = await CreateAdapterAsync();

        foreach (var (request, response) in requests)
        {
            var genericMethod = typeof(TestUtils)
                .GetMethod(nameof(MockGenericRequest), BindingFlags.Static | BindingFlags.NonPublic)!
                .MakeGenericMethod(request.GetType(), response.GetType());

            genericMethod.Invoke(null, new[] { backend, request, response });
        }

        var serializerSettings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            Converters =
            {
                new TestAttachmentConverter()
            }
        };

        serializerSettings.AddReplikitConverters();

        var result = await action(adapter);
        var serializedResult = JsonSerializer.Serialize(result, serializerSettings);

        // ReSharper disable once ExplicitCallerInfoArgument
        await VerifyJson(serializedResult, sourceFile: sourceFile)
            .DisableDiff()
            .UseDirectory("__snapshots__");
    }
}
