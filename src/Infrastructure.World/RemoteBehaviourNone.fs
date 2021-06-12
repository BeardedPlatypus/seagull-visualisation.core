namespace Seagull.Visualisation.Core.Persistence.World

open Seagull.Visualisation.Core.Domain.World
open Seagull.Visualisation.Core.Application.World

/// <summary>
/// <see cref="RemoteBehaviourNone"/> implements the <see cref="IRemoteBehaviour"/>
/// to raise a not supported exception.
/// </summary>
[<Sealed>]
type public RemoteBehaviourNone =
    interface IRemoteBehaviour with 
        member _.RetrieveTile (_: Tile.Index) : Tile.Image = 
            raise <| System.NotSupportedException($"Cannot retrieve tiles with a {nameof(RemoteBehaviourNone)}")

    interface System.IDisposable with
        member this.Dispose() = ()
  
