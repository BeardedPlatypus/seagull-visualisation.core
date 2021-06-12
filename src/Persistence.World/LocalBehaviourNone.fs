namespace Seagull.Visualisation.Core.Persistence.World

open Seagull.Visualisation.Core.Domain.World
open Seagull.Visualisation.Core.Application.World

/// <summary>
/// <see cref="LocalBehaviourNone"/> implements the <see cref="ILocalBehaviour"/>
/// to always return none.
/// </summary>
[<Sealed>]
type public LocalBehaviourNone =
    interface ILocalBehaviour with 
        member _.RetrieveTile (_: Tile.Index) : Tile.Image Option = 
            None

        member _.StoreTile (_: Tile.Index) (_: Tile.Image) : unit =
            ()
  
