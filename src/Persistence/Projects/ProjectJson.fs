namespace Seagull.Visualisation.Core.Persistence.Projects

open PathLib

open Seagull.Visualisation.Core
open Seagull.Visualisation.Core.Persistence.Utilities

open Project

module internal ProjectJson =
    let createNewProject (path: IPath) : unit =
        { Version = Version.FromDomain Domain.Projects.ProjectVersion.CurrentVersion }
        |> Json.toObject
        |> Json.writeObj path
