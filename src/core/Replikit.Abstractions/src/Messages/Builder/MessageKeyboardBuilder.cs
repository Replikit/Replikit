using Replikit.Abstractions.Messages.Models.Keyboard;

namespace Replikit.Abstractions.Messages.Builder;

public class MessageKeyboardBuilder : MessageKeyboardBuilder<MessageKeyboardBuilder> { }

public class MessageKeyboardBuilder<TBuilder> where TBuilder : MessageKeyboardBuilder<TBuilder>
{
    private bool _shouldRemove;
    public ButtonMatrixBuilder<KeyboardButton> ButtonMatrixBuilder { get; } = new();

    public TBuilder Remove()
    {
        _shouldRemove = true;

        return (TBuilder) this;
    }

    public MessageKeyboard Build()
    {
        return new MessageKeyboard(_shouldRemove, ButtonMatrixBuilder.Build());
    }
}
