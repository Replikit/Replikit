# Routing

- [Getting started](getting-started.md)
- [Controllers](controllers.md)
- [Messages](messages.md)
- Routing
- [Adapter services](adapter-services.md)
- [Further reading](further-reading.md)

Replikit can handle not only incoming messages, but also any other types of events. There are **event handlers** for
this task - less convenient than controllers, but more flexible and universal.

For example, we can easily handle button clicks. Just define a handler class inherited
from `AdapterEventHandler<TEvent>` and it will be registered automatically:

```c#
public class ButtonLoggingMiddleware : AdapterEventHandler<ButtonPressedEvent>
{
    private readonly ILogger<ButtonLoggingMiddleware> _logger;

    public ButtonLoggingMiddleware(ILogger<ButtonLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public override async Task<Unit> Handle(IEventContext<ButtonPressedEvent> context, NextAction next)
    {
        _logger.LogDebug("Data: {Data}", context.Event.Data);
        return await next();
    }
}
```

This handler simply logs the button payload. Note that it calls and awaits the `next()` method to allow the rest of the
handlers to process the request.

Let's see the another example:

```c#
public class TextLoggingMiddleware : MessageEventHandler<MessageEvent>
{
    private readonly ILogger<TextLoggingMiddleware> _logger;

    public TextLoggingMiddleware(ILogger<TextLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public override Task<Unit> Handle(IEventContext<MessageEvent> context, NextAction next)
    {
        _logger.LogDebug("Handling message with text: {Text}", Message.Text);
        return next();
    }
}
```

This one, unlike the previous one, does not handle a specific event, but all events derived from `MessageEvent`. Note
that `MessageEventHandler` is used as the base handler class, providing convenient shortcut-properties.

Also note that all event handlers have a transient lifetime. They will be lazily created on demand. This means you can
use scoped services as handler dependencies.

Read next: [Adapter services](adapter-services.md)
