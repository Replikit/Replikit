# Messages

- [Getting started](getting-started.md)
- [Controllers](controllers.md)
- Messages
- [Routing](routing.md)
- [Adapter services](adapter-services.md)
- [Further reading](further-reading.md)

Up to this point, we have only sent text messages, but the possibilities of Replikit are not limited to them only. In
this section, we will look at the tools that allow you to send messages with attachments and some extra elements.

Let's go back to the example of a controller returning text:

```c#
public class HelloController : Controller
{
    [Pattern("hi")]
    public OutMessage Hello() => $"Hello, {Account.Username}!";
}
```

Note that the return type of the method is `OutMessage`. It is the type that determines the content of the message, and
the string is simply implicitly converted to it. You can create an object of this type manually, using the constructor,
but there are more convenient ways to do this.

The most powerful tool for creating messages is the `MessageBuilder` class:

```c#
public class HelloController : Controller
{
    [Pattern("hi")]
    public OutMessage Hello() => new MessageBuilder()
        .AddText("Hello, ")
        .AddText(Account.Username, TextModifiers.Bold);
}
```

In addition to text, the message builder also allows you to add attachments:

```c#
public class AttachmentController : Controller
{
    [Command("photo with text")]
    public OutMessage GetPhotoWithText() => new MessageBuilder()
        .AddText("Hi")
        .WithAttachment(PhotoAttachment.FromUrl("https://picsum.photos/512"));
}
```

Note that the static `FromUrl` method is used to create the attachment. All derived attachment types have such methods.

Also the message builder allows you to add a keyboard to the message:

```c#
public class KeyboardController : Controller
{
    [Command("keyboard")]
    public OutMessage GetKeyboard() => new MessageBuilder()
        .AddText("Test keyboard")
        .WithKeyboard(keyboard => keyboard
            .AddButtonRow("Test 1", "Test 2")
            .AddButtonRow()
            .AddButton("Test 3")
            .AddButton("Test 4"));

    [Command("remove keyboard")]
    public OutMessage RemoveKeyboard() => new MessageBuilder()
        .AddText("Test keyboard removed")
        .WithKeyboard(keyboard => keyboard.Remove());
}
```

Please note that on many platforms, sending a keyboard alone is not enough to send a message - it must be with text or
an attachment.

In most cases, you will need to create simple messages consisting of plain text or an attachment, so Replikit provides
static factory methods and automatic conversions.

For example, the methods for creating attachments discussed earlier look much more interesting with automatic conversion
to `OutMessage`:

```c#
public class AttachmentController : Controller
{
    [Command("photo")]
    public OutMessage GetPhoto() => PhotoAttachment.FromUrl("https://picsum.photos/512");
}
```

In addition to these methods, there are also `OutMessage.FromCode` and ` OutMessage.FromAttachments`.

Read next: [Routing](routing.md)
