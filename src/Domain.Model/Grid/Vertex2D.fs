namespace Seagull.Visualisation.Core.Domain.Model

/// <summary>
/// <see cref="Vertex2D"/> defines a two dimensional vertex consisting of a x 
/// and y coordinate.
/// </summary> 
[<Sealed>]
type public Vertex2D (x: double, y: double) =
    /// <summary>
    /// Gets the X-coordinate of this <see cref="Vertex2D"/>
    /// </summary>
    member val public X: double = x with get

    /// <summary>
    /// Gets the Y-coordinate of this <see cref="Vertex2D"/>
    /// </summary>
    member val public Y: double = y with get

