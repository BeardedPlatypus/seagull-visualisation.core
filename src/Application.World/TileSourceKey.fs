namespace Seagull.Visualisation.Core.Application.World

/// <summary>
/// <see cref="TileSourceKey"/> defines the key of a tile source
/// as some id and a readable name.
/// </summary>
[<Struct>]
type public TileSourceKey (name: string, id: int) =
    /// <summary>The human-readable name of the tilesource.</summary> 
    member x.readableName: string = name

    /// <summary>The id of the tile source.</summary>
    member x.Id: int = id
