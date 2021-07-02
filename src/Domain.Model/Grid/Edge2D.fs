namespace Seagull.Visualisation.Core.Domain.Model.Grid

/// <summary>
/// <see cref="Edge2D"/> defines a two-dimensional edge, where the indices
/// refer to two vertices within a mesh.
/// </summary>
/// <remarks>
/// <see cref="IndexA"/> and <see cref="IndexB"/> are expected to be different.
/// </remarks>
[<Sealed>]
type public Edge2D (indexA: int, indexB: int) =
    /// <summary>
    /// The first index of this <see cref="Edge2D"/>.
    /// </summary>
    member val public IndexA: int = indexA

    /// <summary>
    /// The second index of this <see cref="Edge2D"/>.
    /// </summary>
    member val public IndexB: int = indexB

