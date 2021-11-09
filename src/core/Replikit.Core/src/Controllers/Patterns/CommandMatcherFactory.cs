using System.Text;
using System.Text.RegularExpressions;
using Kantaiko.Controllers.Design.Endpoints;
using Kantaiko.Controllers.Design.Parameters;
using Kantaiko.Controllers.Design.Properties;
using Kantaiko.Controllers.Matchers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;
using Replikit.Core.Options;

namespace Replikit.Core.Controllers.Patterns;

internal static class CommandMatcherFactory
{
    public static IEndpointMatcher<IEventContext<MessageReceivedEvent>> CreateEndpointMatcher(
        EndpointDesignContext context,
        string[] aliases)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<ControllersOptions>>();

        var patternBuilder = new StringBuilder();

        patternBuilder.AppendGroup(options.Value.Prefixes.Select(Regex.Escape));

        if (context.Info.Controller.Properties.TryGetValue(ReplikitControllerProperties.CommandGroupName,
                out var value))
        {
            var commandGroupName = (string) value;
            patternBuilder.Append(commandGroupName);
            patternBuilder.Append(' ');
        }

        patternBuilder.AppendGroup(aliases);

        foreach (var parameter in context.Info.Parameters)
        {
            if (parameter.Properties.TryGetProperty<bool>(KantaikoParameterProperties.IsHidden, out var isHidden) &&
                isHidden)
                continue;

            if (!parameter.Properties.TryGetProperty<IReadOnlyList<string>>(ReplikitParameterProperties.ParameterNames,
                    out var parameterNames))
            {
                patternBuilder.Append(parameter.IsOptional ? @"\s?" : ' ');
                patternBuilder.AddParameter(parameter.Name);
                continue;
            }

            foreach (var parameterName in parameterNames)
            {
                patternBuilder.Append(parameter.IsOptional ? @"\s?" : ' ');
                patternBuilder.AddParameter(parameterName);
            }
        }

        var pattern = patternBuilder.ToString();
        return new RegexMatcher(pattern);
    }

    private static void AddParameter(this StringBuilder patternBuilder, string name)
    {
        patternBuilder.Append("(?<");
        patternBuilder.Append(name);
        patternBuilder.Append(@">\S+)");
    }

    private static void AppendGroup(this StringBuilder patternBuilder, IEnumerable<string> values)
    {
        patternBuilder.Append("(?:");
        patternBuilder.AppendJoin("|", values.Select(Regex.Escape));
        patternBuilder.Append(')');
    }
}
