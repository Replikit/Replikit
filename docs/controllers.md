# Controllers

- [Getting started](getting-started.md)
- Controllers
- [Messages](messages.md)
- [Routing](routing.md)
- [Adapter services](adapter-services.md)
- [Further reading](further-reading.md)

Controllers are main building blocks of all Replikit applications. In this section, we'll take a look at the basic
features they provide.

Any controller is just a class that inherits from `Controller` and declares methods decorated with one or more pattern
attributes:

```c#
public class HelloController : Controller
{
    [Pattern("hi")]
    public OutMessage Hello() => $"Hello, {Account.Username}!";
}
```

Such method called **endpoints**. If the endpoint returns an object of type `OutMessage` (or something like that) the
corresponding message will be sent back to the user.

Endpoints can accept parameters:

```c#
public class HelloController : Controller
{
    [Pattern("greet {name}")]
    public OutMessage Hello(string name) => $"Hello, {name}!";
}
```

Endpoint parameters may be optional:

```c#
public class HelloController : Controller
{
    [Pattern("hi")]
    [Pattern("/greet {name}")]
    public OutMessage Hello(string? name = null) => $"Hello, {name ?? Account.Username}!";
}
```

Parameters can be not only strings, but also numbers, booleans, or any other types, if an appropriate converter exists:

```c#
public class SumController : Controller
{
    [Pattern("/sum {a} {b}")]
    public OutMessage Sum(int a, int b) => $"a + b = {a + b}";
}
```

The entire controller infrastructure is built on top of
the [`Kantaiko.Controllers`](https://github.com/Kantaiko/Controllers) library. You can read more about custom converters
in [this section](https://github.com/Kantaiko/Controllers/blob/master/docs/parameter-conversion.md).

Besides the `[Pattern]` attribute, the `[Regex]` and `[Command]` attributes are also available out of the box. The first
one allows you to use a regular expression as a template:

```c#
public class SumController : Controller
{
    [Regex(@"/sum (?<a>\d+) (?<b>\d+)")]
    public OutMessage Sum(int a, int b) => $"a + b = {a + b}";
}
```

And the second generates it automatically based on the method parameters:

```c#
public class SumController : Controller
{
    [Command("sum")]
    public OutMessage Sum(int a, int b) => $"a + b = {a + b}";
}
```

As you may have noticed, all three of the last examples do the same thing, but the last one is the easiest to use and
preferred.

Another feature of controllers is the ability to validate parameters using attributes:

```c#
public class SumController : Controller
{
    [Command("sum")]
    public OutMessage Sum([MinValue(0)] int a, [MaxValue(10)] int b) => $"a + b = {a + b}";
}
```

And of course you can create your own validators, more details in
this [section](https://github.com/Kantaiko/Controllers/blob/master/docs/parameter-post-validation.md).

Read next: [Messages](messages.md)
