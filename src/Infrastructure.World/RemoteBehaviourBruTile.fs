namespace Seagull.Visualisation.Core.Persistence.World

open Seagull.Visualisation.Core.Domain.World
open Seagull.Visualisation.Core.Application.World

/// <summary>
/// <see cref="RemoteBehaviourBruTile"/> implements the <see cref="IRemoteBehaviour"/>
/// to download tile images from a <see cref="BruTile.ITileSource"/>.
/// </summary>
[<Sealed>]
type public RemoteBehaviourBruTile (source: BruTile.ITileSource) =
    let mutable hasDisposed : bool = false 

    interface IRemoteBehaviour with 
        member this.RetrieveTile (index: Tile.Index) : Tile.Image = 
            let info = BruTile.TileInfo()
            info.Index <- BruTile.TileIndex(index.X, index.Y, index.ZoomLevel)

            source.GetTile(info) |> Tile.Image

    interface System.IDisposable with
        member this.Dispose() = 
            if not hasDisposed then
                match source with 
                | :? System.IDisposable as vDisp -> vDisp.Dispose()
                | _ -> ()

                ( hasDisposed <- true ) |> ignore
  

