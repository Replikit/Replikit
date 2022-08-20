namespace Replikit.Abstractions.Common.CustomData;

/// <summary>
/// Describes something containing some custom data.
/// This interface can be implemented by events and any other "incoming" Replikit models.
/// <br/>
/// This data is populated by an adapter and can contain some information about an entity
/// that is not available in the corresponding Replikit model.
/// <br/>
/// This mechanism allows client code to access objects
/// which the adapter received from the underlying library and mapped to Replikit models.
/// </summary>
public interface IHasCustomData
{
    /// <summary>
    /// The collection of custom data objects associated with this object.
    /// </summary>
    IReadOnlyList<object> CustomData { get; }
}
