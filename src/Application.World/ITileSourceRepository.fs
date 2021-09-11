namespace Seagull.Visualisation.Core.Application.World

/// <summary>
/// <see cref="ITileSourceRepository"/> defines the interface with
/// which to retrieve tiles from a single source.
/// </summary>
[<Interface>]
type public ITileSourceRepository =
    inherit System.IDisposable

    /// <summary>
    /// Retrieve the names of all tile sources stored in this 
    /// <see cref="ITileSourceRepository"/>.
    /// </summary>
    /// <returns>
    /// A sequence of tile source names available within this 
    /// <see cref="ITileSourceRepository"/>.
    /// </returns>
    abstract member RetrieveTileSourceKeys : unit -> TileSourceKey seq

    /// <summary>
    /// Retrieve the tile source associated with the specified 
    /// <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key of the tile source.</param>
    /// <exception cref="System.ArgumentException">
    /// NOT (<paramref name="key"/> IN RetrieveTileSourceKeys)
    /// </exception>
    abstract member RetrieveTileSource : key:TileSourceKey -> TileSource

