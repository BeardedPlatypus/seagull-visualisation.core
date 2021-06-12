namespace Seagull.Visualisation.Core.Application.World

open Seagull.Visualisation.Core.Domain.World

/// <summary>
/// <see cref="ILocalBehaviour"/> defines the interface with which to 
/// retrieve and store tiles locally.
/// </summary>
[<Interface>]
type public ILocalBehaviour = 
    /// <summary>
    /// Retrieve the local tile at <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The index of the tile to retrieve.</param>
    /// <returns>
    /// If the tile is stored locally then the <see cref="Tile.Image"/> associated 
    /// with the parameters. Otherwise: None.
    /// </returns>
    abstract member RetrieveTile : index:Tile.Index -> Tile.Image Option

    /// <summary>
    /// Store the <paramref name="image"/> defined by the <paramref name="index"/>.
    /// </summary>
    /// <param name="source">The source from which the image was obtained.</param>
    /// <param name="index">The index of the tile to store.</param>
    /// <param name="image">The image of the tile to store.</param>
    abstract member StoreTile : index:Tile.Index -> image:Tile.Image -> unit

