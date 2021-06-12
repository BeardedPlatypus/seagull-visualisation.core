namespace Seagull.Visualisation.Core.Application.World

open Seagull.Visualisation.Core.Domain.World

/// <summary>
/// <see cref="IRemoteBehaviour"/> defines the interface with which to 
/// retrieve tiles from a remote source.
/// </summary>
[<Interface>]
type public IRemoteBehaviour = 
    inherit System.IDisposable

    /// <summary>
    /// Get the tile at the <paramref name="index"/> .
    /// </summary>
    /// <param name="index">The index of the tile image.</param>
    /// <returns>
    /// The image of the tile at <paramref name="index"/>.
    /// </returns>
    abstract member RetrieveTile : index:Tile.Index -> Tile.Image

