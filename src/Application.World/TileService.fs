namespace Seagull.Visualisation.Core.Application.World

open Seagull.Visualisation.Core.Domain.World

/// <summary>
/// <see cref="IRepository"/> defines the TileSource repository interface with
/// which tiles can be retrieved and cached.
/// </summary>
[<Sealed>]
type public TileService (localTileService: ILocalTileService, 
                         remoteTileService: IRemoteTileService,
                         sources: Tile.Source list) =
    let mutable hasDisposed : bool = false 
    
    let mutable activeSource : Tile.Source = sources |> List.head

    interface ITileService with
        member this.GetActiveSourceName(): string = activeSource.ToStringRepr()

        member this.GetSourceNames(): seq<string> = 
            sources |> List.map (fun f -> f.ToStringRepr()) |> List.toSeq
             

        member this.GetTile(x: int) (y: int) (zoomLevel: int): byte [] = 
            let tileIndex = { Tile.Index.X = x; Tile.Index.Y = y; Tile.Index.ZoomLevel = zoomLevel }
            let localTile = localTileService.GetLocalTile activeSource tileIndex

            match localTile with 
            | Some v -> v.toBytes ()
            | None ->
                let remoteTile = remoteTileService.GetRemoteTile activeSource tileIndex
                localTileService.StoreLocalTile activeSource tileIndex remoteTile

                remoteTile.toBytes ()

        member this.SetActiveSource(name: string): unit = 
            let source = Tile.Source name
            if List.contains source sources then
                activeSource <- source

        member this.Dispose (): unit = 
            if not hasDisposed then 
                ( remoteTileService :> System.IDisposable ).Dispose()

                ( hasDisposed <- true ) |> ignore
