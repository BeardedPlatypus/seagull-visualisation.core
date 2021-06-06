namespace Seagull.Visualisation.Core.Application.World

open Seagull.Visualisation.Core.Domain.World

[<Interface>]
type public ILocalTileService = 
    abstract member GetLocalTile : source:Tile.Source -> index:Tile.Index -> Tile.Image Option
    abstract member StoreLocalTile : source:Tile.Source -> index:Tile.Index -> image:Tile.Image -> unit

