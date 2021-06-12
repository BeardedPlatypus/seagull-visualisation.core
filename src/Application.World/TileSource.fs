namespace Seagull.Visualisation.Core.Application.World

/// <summary>
/// <see cref="ITileSource"/> defines the interface with
/// which to retrieve tiles from a single source.
/// </summary>
[<Sealed>]
type public TileSource (name: string, 
                        minZoom: int, 
                        maxZoom: int) = 
    let mutable hasDisposed = false

    /// <summary>
    /// Gets the name of this <see cref="ITileSource"/>
    /// </summary>
    member val Name : string = name with get

    /// <summary>
    /// Gets the minimum zoom of this <see cref="ITileSource"/>
    /// </summary>
    member val MinZoom : int = minZoom with get

    /// <summary>
    /// Gets the maximum zoom of this <see cref="ITileSource"/>
    /// </summary>
    member val MaxZoom : int = maxZoom with get

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
    member this.RetrieveTile (x: int) (y: int) (zoomLevel: int) : byte[] =
        Array.empty

    interface System.IDisposable with
        member this.Dispose() = ()

        
