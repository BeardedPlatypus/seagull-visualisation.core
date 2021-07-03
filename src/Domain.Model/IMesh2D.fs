namespace Seagull.Visualisation.Core.Domain.Model

/// <summary>
/// <see cref="IMesh2D"/> defines a two-dimensional grid consisting of
/// vertices (nodes) and faces consisting of three or more vertices.
/// </summary>
[<Interface>]
type public IMesh2D =
    /// <summary>
    /// Retrieve the name of this <see cref="IMesh2D"/>.
    /// </summary>
    /// <returns>
    /// The name of this <see cref="IMesh2D"/>.
    /// </returns>
    abstract member RetrieveName: unit -> string

    /// <summary>
    /// Retrieve the vertices of this <see cref="IMesh2D"/>.
    /// </summary>
    /// <returns>
    /// The vertices of this <see cref="IMesh2D"/>.
    /// </returns>
    abstract member RetrieveVertices: unit -> seq<Grid.Vertex2D>

    /// <summary>
    /// Retrieve the edges of this <see cref="IMesh2D"/>.
    /// </summary>
    /// <returns>
    /// The edges of this <see cref="IMesh2D"/>.
    /// </returns>
    /// <remarks>
    /// Note that the indices of the <see cref="Grid.Edge2D"/> refer to the
    /// vertices of <see cref="RetrieveVertices"/>.
    /// </remarks>
    abstract member RetrieveEdges: unit -> seq<Grid.Edge2D>

    /// <summary>
    /// Retrieve the faces of this <see cref="IMesh2D"/>.
    /// </summary>
    /// <returns>
    /// The faces of this <see cref="IMesh2D"/>.
    /// </returns>
    /// <remarks>
    /// Note that the indices of the <see cref="Grid.Face2D"/> refer to the
    /// vertices of <see cref="RetrieveVertices"/>.
    /// </remarks>
    abstract member RetrieveFaces: unit -> seq<Grid.Face2D>

