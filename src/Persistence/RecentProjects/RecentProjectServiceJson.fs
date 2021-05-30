namespace Seagull.Visualisation.Core.Persistence

open Seagull.Visualisation.Core.Application
open Seagull.Visualisation.Core.Persistence.AppDataRepository
open Seagull.Visualisation.Core.Persistence.RecentProjects

/// <summary>
/// Get the recent projects
/// </summary>
type RecentProjectServiceJson (appDataRepository: IAppDataRepository) =
    let appDataRepository = appDataRepository
    
    interface IRecentProjectsService with
        member this.GetRecentProjects() =
            appDataRepository.Query RecentProject.RecentProjectsKey
        
        member this.UpdateRecentProject(recentProject) = failwith "todo"
            
        member this.RemoveRecentProject(recentProject) = failwith "todo"
    
    
    