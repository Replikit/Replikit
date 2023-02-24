using System.Text.Json;

namespace Replikit.Core.Serialization.Json;

/// <summary>
/// The interface for JSON serializer used by Replikit and its extensions.
/// </summary>
public interface IReplikitJsonSerializer
{
    /// <summary>
    /// Serializes the specified object to JSON.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <returns>The JSON string.</returns>
    /// <exception cref="JsonException">An error occurred while serializing the object.</exception>
    string Serialize(object? value);

    /// <summary>
    /// Deserializes the specified JSON string to an object of the specified type.
    /// </summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <returns>The deserialized object.</returns>
    T? Deserialize<T>(string json);
}
