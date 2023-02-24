using System.Dynamic;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Core.Serialization.Dynamic;

namespace Replikit.Core.Sessions;

/// <summary>
/// The main implementation of <see cref="ISession"/>.
/// It is implemented as a dictionary of <see cref="DynamicObject"/>s.
/// </summary>
public class Session : ISession
{
    private readonly Dictionary<string, DynamicValue> _data;
    private readonly Dictionary<string, object?> _deserializedData = new();

    /// <summary>
    /// The dictionary of <see cref="DynamicObject"/>s which can be used to serialize the session.
    /// </summary>
    public IReadOnlyDictionary<string, DynamicValue> Data => _data;

    /// <summary>
    /// The flag that indicates whether the session was accessed and it's data may be changed.
    /// </summary>
    public bool WasAccessed { get; private set; }

    /// <summary>
    /// Creates a new instance of <see cref="Session"/>.
    /// </summary>
    /// <param name="data">The dictionary of <see cref="DynamicObject"/>s to deserialize the session.</param>
    public Session(IReadOnlyDictionary<string, DynamicValue> data)
    {
        Check.NotNull(data);

        _data = data.ToDictionary(x => x.Key, x => x.Value);
    }

    /// <summary>
    /// Creates an empty instance of <see cref="Session"/>.
    /// </summary>
    public Session()
    {
        _data = new Dictionary<string, DynamicValue>();
    }

    public TValue? GetValue<TValue>(string key)
    {
        if (_deserializedData.TryGetValue(key, out var value))
        {
            return (TValue?) value;
        }

        if (!_data.TryGetValue(key, out var dynamicValue))
        {
            _deserializedData[key] = default;
            return default;
        }

        var deserializedValue = dynamicValue.Deserialize<TValue>();
        _deserializedData[key] = deserializedValue;
        WasAccessed = true;

        return deserializedValue;
    }

    public void SetValue<TValue>(string key, TValue? value)
    {
        WasAccessed = true;

        if (value is null || EqualityComparer<TValue>.Default.Equals(value, default))
        {
            _data.Remove(key);
            _deserializedData.Remove(key);
        }
        else
        {
            _deserializedData[key] = value;
            _data[key] = DynamicValue.FromValue(value);
        }
    }
}
