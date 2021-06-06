namespace Seagull.Visualisation.Core.Infrastructure.World

open Seagull.Visualisation.Core.Application.World
open Seagull.Visualisation.Core.Domain.World

[<Sealed>]
type public RemoteTileServiceBruTile (sources: BruTile.ITileSource list) =
    let mutable hasDisposed : bool = false 

    let sourceMapping : Map<string, BruTile.ITileSource> =
        sources |> List.map (fun s -> (s.Name, s)) |> Map

    interface IRemoteTileService with
        member this.GetRemoteTile (source: Tile.Source) (index: Tile.Index) : Tile.Image =
            let source = sourceMapping.[(source.ToStringRepr())]

            let info = BruTile.TileInfo()
            info.Index <- BruTile.TileIndex(index.X, index.Y, index.ZoomLevel)
        
            source.GetTile(info) |> Tile.Image

        member this.Dispose(): unit = 
            if not hasDisposed then
                for s in sources do 
                    match s with 
                    | :? System.IDisposable as vDisp -> vDisp.Dispose()
                    | _ -> ()

                ( hasDisposed <- true ) |> ignore
