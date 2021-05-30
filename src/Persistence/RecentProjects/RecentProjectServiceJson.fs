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
                |> Seq.filter (fun (v: RecentProject.T) -> v.Path <> rpValue.Path)
                |> Seq.append [ rpValue ]
                |> Seq.sortBy (fun v -> v.LastOpened)
                |> writeRecentProjects
            | Result.Error _ -> () 
        
        member this.RemoveRecentProject(recentProject) =
            let path: string = recentProject.Path.ToString()
            
            match getRecentProjects () with
            | Result.Ok res  ->
                res
                |> Seq.filter (fun (v: RecentProject.T) -> v.Path <> path)
                |> writeRecentProjects
            | Result.Error _ -> () 
        
    
    
    