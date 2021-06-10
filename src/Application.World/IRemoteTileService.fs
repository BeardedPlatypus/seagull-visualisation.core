namespace Seagull.Visualisation.Core.Application.World

open Seagull.Visualisation.Core.Domain.World

/// <summary>
/// <see cref="ILocalTileService"/> defines the interface with which to 
/// retrieve and from an external source.
/// </summary>
[<Interface>]
type public IRemoteTileService = 
    inherit System.IDisposable

    // TODO: this should probably return results and incorporate handlers for success and errors.
    /// <summary>
    /// Get the tile at the <paramref name="index"/> from the specified 
    /// <paramref name="index"/>.
    /// </summary>
    /// <param name="source">The source from which to retrieve the image.</param>
    /// <param name="index">The index of the tile image.</param>
    /// <returns>
    /// The image of the tile at <paramref name="index"/> retrieved from the
    /// <paramref name="source"/>.
    /// </returns>
    abstract member GetRemoteTile : source:Tile.Source -> index:Tile.Index -> Tile.Image


