namespace Seagull.Visualisation.Core.Application.World

/// <summary>
/// <see cref="TileSourceKey"/> defines the key of a tile source
/// as some id and a readable name.
/// </summary>
type public TileSourceKey =
    struct 
        /// <summary>The human-readable name of the tilesource.</summary> 
        val readableName: string 

        /// <summary>The id of the tile source.</summary>
        val id: int
    end
