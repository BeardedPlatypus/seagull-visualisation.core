﻿namespace Seagull.Visualisation.Core.Persistence.RecentProjects

open Seagull.Visualisation.Core
open Seagull.Visualisation.Core.Persistence.AppDataRepository
open Seagull.Visualisation.Core.Persistence.RecentProjects

/// <summary>
/// Get the recent projects
/// </summary>
[<Sealed>]
type public RecentProjectServiceJson (appDataRepository: IAppDataRepository) =
    let appDataRepository = appDataRepository
    
    let getRecentProjects () =
        appDataRepository.Query RecentProject.Key Seq.empty
        
    let writeRecentProjects (recentProjects: seq<RecentProject.T>) : unit =
        appDataRepository.Write RecentProject.Key recentProjects
        
    interface Application.IRecentProjectService with
        member this.GetRecentProjects () = 
            getRecentProjects ()
            
        member this.UpdateRecentProject(recentProject) =
            let rpValue = RecentProject.T.FromDomain recentProject
            
            getRecentProjects ()
            |> RecentProject.updateRecentProjects rpValue
            |> writeRecentProjects
        
        member this.RemoveRecentProject(recentProject) =
            let rpValue = RecentProject.T.FromDomain recentProject

            getRecentProjects ()
            |> RecentProject.removeRecentProject rpValue
            |> writeRecentProjects