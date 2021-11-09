using Replikit.Abstractions.Events;

namespace Replikit.Core.Handlers.Internal;

internal interface IAutoRegistrableHandler { }

internal interface IAutoRegistrableHandler<TEvent> : IAutoRegistrableHandler where TEvent : IEvent { }
