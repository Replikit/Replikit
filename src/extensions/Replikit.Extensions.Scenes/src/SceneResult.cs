using System.Linq.Expressions;
using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.Scenes.Messages;

namespace Replikit.Extensions.Scenes;

public class SceneResult
{
    public SceneResult() { }

    public SceneResult(SceneMessageBuilder sceneMessageBuilder, bool shouldExit = false)
    {
        SceneMessageBuilder = sceneMessageBuilder;
        ShouldExit = shouldExit;
    }

    public SceneResult(OutMessage outMessage, bool shouldExit = false)
    {
        OutMessage = outMessage;
        ShouldExit = shouldExit;
    }

    public SceneResult(bool shouldExit)
    {
        ShouldExit = shouldExit;
    }

    public SceneResult(Expression stage, OutMessage? outMessage = null)
    {
        OutMessage = outMessage;
        Transition = stage;
    }

    public bool ShouldExit { get; }
    public SceneMessageBuilder? SceneMessageBuilder { get; }
    public OutMessage? OutMessage { get; }
    public Expression? Transition { get; }

    public static implicit operator SceneResult(OutMessage outMessage) => new(outMessage);
    public static implicit operator SceneResult(MessageBuilder messageBuilder) => new(messageBuilder);
    public static implicit operator SceneResult(SceneMessageBuilder sceneMessageBuilder) => new(sceneMessageBuilder);
    public static implicit operator SceneResult(string text) => new(text);
}
