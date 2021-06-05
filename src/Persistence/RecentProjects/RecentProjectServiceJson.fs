namespace Seagull.Visualisation.Core.Persistence

open Seagull.Visualisation.Core
open Seagull.Visualisation.Core.Persistence.AppDataRepository
open Seagull.Visualisation.Core.Persistence.RecentProjects

/// <summary>
/// Get the recent projects
/// </summary>
type RecentProjectServiceJson (appDataRepository: IAppDataRepository) =
    let appDataRepository = appDataRepository
    
    let getRecentProjects () =
        appDataRepository.Query<seq<RecentProject.T>> RecentProject.Key
        
    let writeRecentProjects (recentProjects: seq<RecentProject.T>) : unit =
        appDataRepository.Write RecentProject.Key recentProjects
        
    interface Application.IRecentProjectsService with
        member this.GetRecentProjects () =
            match getRecentProjects () with
            | Result.Ok res  -> Seq.map (fun (v: RecentProject.T) -> v.ToDomain()) res
            | Result.Error _ -> Seq.empty
            
        member this.UpdateRecentProject(recentProject) =
            let rpValue = RecentProject.T.FromDomain recentProject
            
            match getRecentProjects () with
            | Result.Ok res  ->
                res
                |> RecentProject.updateRecentProjects rpValue
                |> writeRecentProjects
            | Result.Error _ -> () 
        
        member this.RemoveRecentProject(recentProject) =
            let rpValue = RecentProject.T.FromDomain recentProject

            match getRecentProjects () with
            | Result.Ok res  ->
                res
                |> RecentProject.removeRecentProject rpValue
                |> writeRecentProjects
            | Result.Error _ -> () 