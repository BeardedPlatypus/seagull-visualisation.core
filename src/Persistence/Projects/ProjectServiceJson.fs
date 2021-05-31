﻿namespace Seagull.Visualisation.Core.Persistence.Projects

open Seagull.Visualisation.Core.Application

open PathLib

type ProjectServiceJson () =
    interface IProjectService with
        member this.CreateProject(newProjectPath: IPath) : unit =
            Project.createNewProject newProjectPath
