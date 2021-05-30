namespace Seagull.Visualisation.Core.Persistence.RecentProjects

open System
open PathLib
open Seagull.Visualisation.Core

module internal RecentProject =
    let internal Key = "RecentProjects"
    
    [<RequireQualifiedAccess>]
    type internal T =
        {
           LastOpened: DateTime
           Path: string }
        static member internal FromDomain (rp: Domain.RecentProject) : T =
            { LastOpened = rp.LastOpened
              Path = rp.Path.ToString() }
        member internal this.ToDomain () : Domain.RecentProject =
            Domain.RecentProject(PurePath.Create this.Path, this.LastOpened)
            

