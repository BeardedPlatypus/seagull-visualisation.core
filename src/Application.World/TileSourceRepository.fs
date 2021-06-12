namespace Seagull.Visualisation.Core.Application.World

/// <summary>
/// <see cref="ITileSource"/> defines the interface with
/// which to retrieve tiles from a single source.
/// </summary>
[<Sealed>]
type public TileSourceRepository (sources: TileSource seq) =
    let mutable hasDisposed : bool = false
    let sourcesList : TileSource list = sources |> List.ofSeq

    interface ITileSourceRepository with
        member this.RetrieveTileSourceKeys () : TileSourceKey seq =
            sourcesList
            |> List.mapi (fun (i: int) (elem: TileSource) -> TileSourceKey(elem.Name, i))
            |> List.toSeq

        member this.RetrieveTileSource (key: TileSourceKey) : TileSource =
            sourcesList.[key.Id]

    interface System.IDisposable with
        member this.Dispose() = 
            if not hasDisposed then 
                for s in sourcesList do 
                    (s :> System.IDisposable).Dispose ()

