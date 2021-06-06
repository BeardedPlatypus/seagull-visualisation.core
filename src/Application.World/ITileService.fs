namespace Seagull.Visualisation.Core.Application.World

/// <summary>
/// <see cref="IRepository"/> defines the TileSource repository interface with
/// which tiles can be retrieved and cached.
/// </summary>
[<Interface>]
type public ITileService =
    inherit System.IDisposable
    // TODO: Add additional methods to select the tile source here.

    /// <summary>
    /// Get the terrain tile at (<paramref name="x"/>, <paramref name="y"/>) with
    /// the specified <paramref name="zoomLevel"/> from the current tile source.
    /// </summary>
    /// <param name="x">The x-index of the tile.</param>
    /// <param name="y">The y-index of the tile.</param>
    /// <param name="zoomLevel">The zoom level.</param>
    /// <returns>
    /// The tile image as a byte array.
    /// </returns>
    abstract member GetTile : x: int -> y: int -> zoomLevel: int -> byte[]

    /// <summary>
    /// Get the registered names of terrain tile sources.
    /// </summary>
    /// <returns>
    /// The  terrain source names contained in this <see cref="IRepository"/>.
    /// </returns>
    abstract member GetSourceNames : unit -> string seq

    /// <summary>
    /// Get the current active source name.
    /// </summary>
    /// <returns>
    /// The name of the current active source name.
    /// </returns>
    abstract member GetActiveSourceName : unit -> string

    /// <summary>
    /// Set the source to the source corresponding with the 
    /// <paramref name="name"/>.
    /// </summary>
    /// <param name="name">The name of the source to set this repository to. </param>
    /// <exception cref="System.Collections.Generic.KeyNotFoundException">
    /// Thrown when <paramref name="name"/> is not in <see cref="GetSourceNames"/>.
    /// </exception>
    abstract member SetActiveSource : name: string -> unit
