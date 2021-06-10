namespace Seagull.Visualisation.Core.Application.World

open Seagull.Visualisation.Core.Domain.World

/// <summary>
/// <see cref="ILocalTileService"/> defines the interface with which to 
/// retrieve and store tiles locally.
/// </summary>
[<Interface>]
type public ILocalTileService = 
    /// <summary>
    /// Get the local tile at <paramref name="index"/> associated with the 
    /// specified <paramref name="source"/>.
    /// </summary>
    /// <param name="source">The source from which to obtain the image.</param>
    /// <param name="index">The index of the tile to retrieve.</param>
    /// <returns>
    /// If the tile is stored locally then the <see cref="Tile.Image"/> associated 
    /// with the parameters. Otherwise: None.
    /// </returns>
    abstract member GetLocalTile : source:Tile.Source -> index:Tile.Index -> Tile.Image Option

    /// <summary>
    /// Store the <paramref name="image"/> defined by the <paramref name="index"/> obtained
    /// from the <paramref name="source"/> within this <see cref="ILocalTileService"/>.
    /// </summary>
    /// <param name="source">The source from which the image was obtained.</param>
    /// <param name="index">The index of the tile to store.</param>
    /// <param name="image">The image of the tile to store.</param>
    abstract member StoreLocalTile : source:Tile.Source -> index:Tile.Index -> image:Tile.Image -> unit

