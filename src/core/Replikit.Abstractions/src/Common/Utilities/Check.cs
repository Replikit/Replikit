using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Resources;

namespace Replikit.Abstractions.Common.Utilities;

internal static class Check
{
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T NotNull<T>(T argument, [CallerArgumentExpression("argument")] string? argumentName = default)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(argument, argumentName);

        return argument;
    }

    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string NotNullOrWhiteSpace(string argument,
        [CallerArgumentExpression("argument")] string? argumentName = default)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            throw new ArgumentException(Strings.ValueCannotBeNullOrWhiteSpace, argumentName);
        }

        return argument;
    }

    public static TCollection NotNullOrEmpty<TCollection, TItem>(TCollection collection,
        [CallerArgumentExpression("collection")]
        string? argumentName = default)
        where TCollection : IReadOnlyCollection<TItem>
    {
        ArgumentNullException.ThrowIfNull(collection, argumentName);

        if (collection.Count == 0)
        {
            throw new ArgumentException(Strings.CollectionCannotBeEmpty, argumentName);
        }

        return collection;
    }

    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T NotDefault<T>(T argument, [CallerArgumentExpression("argument")] string? argumentName = default)
        where T : struct, IEquatable<T>
    {
        if (argument.Equals(default))
        {
            throw new ArgumentException(Strings.ValueCannotBeDefault, argumentName);
        }

        return argument;
    }

    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? NullOrNotDefault<T>(T? argument,
        [CallerArgumentExpression("argument")] string? argumentName = default)
        where T : struct, IEquatable<T>
    {
        if (argument.HasValue && argument.Value.Equals(default))
        {
            throw new ArgumentException(Strings.ValueCannotBeDefault, argumentName);
        }

        return argument;
    }

    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AttachmentType NotUnknown(AttachmentType argument,
        [CallerArgumentExpression("argument")] string? argumentName = default)
    {
        if (argument is AttachmentType.Unknown)
        {
            throw new ArgumentException(Strings.AttachmentTypeCannotBeUnknown, argumentName);
        }

        return argument;
    }

    public static void Assert([DoesNotReturnIf(false)] bool condition, string argumentName, string message)
    {
        if (!condition)
        {
            throw new ArgumentException(message, argumentName);
        }
    }
}
