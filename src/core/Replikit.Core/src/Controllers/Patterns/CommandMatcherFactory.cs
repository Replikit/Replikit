using System.Text;
using System.Text.RegularExpressions;
using Kantaiko.Controllers.Introspection;
using Kantaiko.Controllers.Introspection.Factory.Context;
using Kantaiko.Controllers.Matching;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Options;

namespace Replikit.Core.Controllers.Patterns;

internal static class CommandMatcherFactory
{
    public static IEndpointMatcher<IEventContext<MessageReceivedEvent>> CreateEndpointMatcher(
        EndpointFactoryContext context, string[] commandNames)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<ControllersOptions>>();

        var patternBuilder = new StringBuilder();

        patternBuilder.Append('^');
        patternBuilder.AppendGroup(options.Value.Prefixes);

        if (CommandGroupControllerProperties.Of(context.Endpoint.Controller!) is { CommandGroupName: { } groupName })
        {
            patternBuilder.Append(groupName);
            patternBuilder.Append(' ');
        }

        patternBuilder.AppendGroup(commandNames);

        foreach (var parameter in context.Endpoint.Parameters)
        {
            if (VisibilityParameterProperties.Of(parameter) is { IsHidden: true })
            {
                continue;
            }

            if (CommandParameterProperties.Of(parameter) is not { ParameterNames: var parameterNames })
            {
                patternBuilder.Append(parameter.IsOptional ? @"\s?" : ' ');
                patternBuilder.AddParameter(parameter.Name, parameter.IsOptional);
                continue;
            }

            foreach (var parameterName in parameterNames)
            {
                patternBuilder.Append(parameter.IsOptional ? @"\s?" : ' ');
                patternBuilder.AddParameter(parameterName, parameter.IsOptional);
            }
        }

        var pattern = patternBuilder.ToString();
        return new RegexMatcher(pattern);
    }

    private static void AddParameter(this StringBuilder patternBuilder, string name, bool isOptional)
    {
        patternBuilder.Append("(?<");
        patternBuilder.Append(name);
        patternBuilder.Append(@">\S");
        patternBuilder.Append(isOptional ? '*' : '+');
        patternBuilder.Append(')');
    }

    private static void AppendGroup(this StringBuilder patternBuilder, IEnumerable<string> values)
    {
        patternBuilder.Append("(?:");
        patternBuilder.AppendJoin("|", values.Select(Regex.Escape));
        patternBuilder.Append(')');
    }
}
