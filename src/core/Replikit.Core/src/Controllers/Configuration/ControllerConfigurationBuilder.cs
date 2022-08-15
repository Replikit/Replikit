using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection.Factory;
using Kantaiko.Controllers.ParameterConversion.Text;
using Replikit.Core.Controllers.Context;

namespace Replikit.Core.Controllers.Configuration;

public class ControllerConfigurationBuilder
{
    public ControllerConfigurationBuilder(IntrospectionBuilder introspectionBuilder,
        IHandlerCollection<IMessageControllerContext> handlers)
    {
        Handlers = handlers;
        IntrospectionBuilder = introspectionBuilder;
    }

    internal HashSet<Type> ConverterLookupTypes { get; } = new();
    public IntrospectionBuilder IntrospectionBuilder { get; }
    public IHandlerCollection<IMessageControllerContext> Handlers { get; }

    public void RegisterConverter<TConverter>() where TConverter : ITextParameterConverter
    {
        ConverterLookupTypes.Add(typeof(TConverter));
    }
}
