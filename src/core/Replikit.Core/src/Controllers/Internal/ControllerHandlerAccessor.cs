using Kantaiko.Controllers;
using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection;
using Kantaiko.Controllers.Introspection.Factory;
using Kantaiko.Controllers.ParameterConversion.Text;
using Kantaiko.Controllers.Result;
using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Modularity.Introspection;
using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Controllers.ExecutionHandlers;

namespace Replikit.Core.Controllers.Internal;

internal class ControllerHandlerAccessor : IControllerIntrospectionInfoAccessor
{
    public IntrospectionInfo IntrospectionInfo { get; }
    public IHandler<IEventContext<MessageReceivedEvent>, Task<ControllerExecutionResult>> Handler { get; }

    public ControllerHandlerAccessor(HostInfo hostInfo, IServiceProvider serviceProvider)
    {
        var lookupTypes = hostInfo.Assemblies.SelectMany(x => x.GetTypes()).ToArray();

        var converterCollection = new TextParameterConverterCollection(lookupTypes);

        var introspectionBuilder = new IntrospectionBuilder<IEventContext<MessageReceivedEvent>>();

        introspectionBuilder.SetServiceProvider(serviceProvider);
        introspectionBuilder.AddDefaultTransformation();
        introspectionBuilder.AddEndpointMatching();
        introspectionBuilder.AddTextParameterConversion(converterCollection);

        IntrospectionInfo = introspectionBuilder.CreateIntrospectionInfo(lookupTypes);

        var pipelineBuilder = new PipelineBuilder<IEventContext<MessageReceivedEvent>>();

        pipelineBuilder.AddEndpointMatching();
        pipelineBuilder.AddSubHandlerExecution();
        pipelineBuilder.AddTextParameterConversion(ServiceHandlerFactory.Instance);
        pipelineBuilder.AddControllerInstantiation(ServiceHandlerFactory.Instance);
        pipelineBuilder.AddHandler(new DispatchControllerInstantiatedEventHandler());
        pipelineBuilder.AddEndpointInvocation();
        pipelineBuilder.AddRequestCompletion();

        var handlers = pipelineBuilder.Build();

        Handler = ControllerHandlerFactory.CreateControllerHandler(IntrospectionInfo, handlers);
    }
}
