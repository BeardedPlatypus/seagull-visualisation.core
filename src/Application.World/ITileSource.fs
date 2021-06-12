namespace Seagull.Visualisation.Core.Application.World

/// <summary>
/// <see cref="ITileSource"/> defines the interface with
/// which to retrieve tiles from a single source.
/// </summary>
type public ITileSource =
    inherit System.IDisposable

    /// <summary>
    /// Gets the name of this <see cref="ITileSource"/>
    /// </summary>
    abstract member Name : string with get

    /// <summary>
    /// Gets the minimum zoom of this <see cref="ITileSource"/>
    /// </summary>
    abstract member MinZoom : int with get

    /// <summary>
    /// Gets the maximum zoom of this <see cref="ITileSource"/>
    /// </summary>
    abstract member MaxZoom : int with get

    /// <summary>
    /// Retrieve the terrain tile at (<paramref name="x"/>, <paramref name="y"/>) with
    /// the specified <paramref name="zoomLevel"/>.
    /// </summary>
    /// <param name="x">The x-index of the tile.</param>
    /// <param name="y">The y-index of the tile.</param>
    /// <param name="zoomLevel">The zoom level.</param>
    /// <returns>
    /// The tile image as a byte array.
    /// </returns>
    abstract member RetrieveTile : x: int -> y: int -> zoomLevel: int -> byte[]

