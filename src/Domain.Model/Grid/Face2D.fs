namespace Seagull.Visualisation.Core.Domain.Model.Grid

/// <summary>
/// <see cref="Face2D"/> defines a two-dimensional face, where the indices
/// refer to two vertices within a mesh.
/// </summary>
/// <remarks>
/// <see cref="Indices"/> are expected to be distinct and in counter-clockwise 
/// order.
/// </remarks>
[<Sealed>]
type public Face2D (indices: seq<int>) =
    let indices: int list = indices |> List.ofSeq

    /// <summary>
    /// Gets the indices of this <see cref="Face2D"/>
    /// </summary>
    member public this.Indices: seq<int> = indices |> Seq.ofList

