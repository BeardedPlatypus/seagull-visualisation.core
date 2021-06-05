namespace Seagull.Visualisation.Core.Persistence.RecentProjects

open System
open PathLib
open Seagull.Visualisation.Core

module public RecentProject =
    let internal Key = "RecentProjects"
    
    [<RequireQualifiedAccess>]
    type public T =
        {
           LastOpened: DateTime
           Path: string }
        static member internal FromDomain (rp: Domain.RecentProject) : T =
            { LastOpened = rp.LastOpened
              Path = rp.Path.ToString() }
        member internal this.ToDomain () : Domain.RecentProject =
            Domain.RecentProject(PurePath.Create this.Path, this.LastOpened)

    let internal removeRecentProject (toRemove: T) (recentProjects: seq<T>) : seq<T> =
         Seq.filter (fun (v: T) -> v.Path <> toRemove.Path) recentProjects
         
    let internal updateRecentProjects (toUpdate: T) (recentProjects: seq<T>) : seq<T> =
        removeRecentProject toUpdate recentProjects
        |> Seq.append [ toUpdate ]
        |> Seq.sortByDescending (fun v -> v.LastOpened)

