namespace Seagull.Visualisation.Core.Persistence.Projects

open Seagull.Visualisation.Core
open Seagull.Visualisation.Core.Persistence.Utilities
open PathLib

module internal Project =
    type Version =
        { Major : int
          Minor : int
          Patch : int }
        static member FromDomain (version: Domain.Projects.ProjectVersion): Version =
            { Major = version.Major
              Minor = version.Minor
              Patch = version.Patch }
    
    type T =
        { Version : Version }
        
    let createNewProject (path: IPath) : unit =
        { Version = Version.FromDomain Domain.Projects.ProjectVersion.CurrentVersion }
        |> Json.toObject
        |> Json.writeObj path
