namespace Seagull.Visualisation.Core.Application.World

open Seagull.Visualisation.Core.Domain.World

[<Interface>]
type public IRemoteTileService = 
    inherit System.IDisposable

    abstract member GetRemoteTile : source:Tile.Source -> index:Tile.Index -> Tile.Image


