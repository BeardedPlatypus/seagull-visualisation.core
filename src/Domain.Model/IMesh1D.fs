namespace Seagull.Visualisation.Core.Domain.Model

/// <summary>
/// <see cref="IMesh1D"/> defines a one-dimensional network consisting of
/// vertices (nodes) and edges connecting these vertices.
/// </summary>
[<Interface>]
type public IMesh1D =
    /// <summary>
    /// Retrieve the name of this <see cref="IMesh1D"/>.
    /// </summary>
    /// <returns>
    /// The name of this <see cref="IMesh1D"/>.
    /// </returns>
    abstract member RetrieveName: unit -> string

    /// <summary>
    /// Retrieve the vertices of this <see cref="IMesh1D"/>.
    /// </summary>
    /// <returns>
    /// The vertices of this <see cref="IMesh1D"/>.
    /// </returns>
    abstract member RetrieveVertices: unit -> seq<Grid.Vertex2D>

    /// <summary>
    /// Retrieve the edges of this <see cref="IMesh1D"/>.
    /// </summary>
    /// <returns>
    /// The edges of this <see cref="IMesh1D"/>.
    /// </returns>
    /// <remarks>
    /// Note that the indices of the <see cref="Grid.Edge2D"/> refer to the
    /// vertices of <see cref="RetrieveVertices"/>.
    /// </remarks>
    abstract member RetrieveEdges: unit -> seq<Grid.Edge2D>

