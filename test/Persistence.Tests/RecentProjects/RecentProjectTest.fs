namespace Seagull.Visualisation.Core.Persistence.Tests.RecentProjects

open NUnit.Framework
open FsUnit

open Seagull.Visualisation.Core.Persistence.RecentProjects
open Seagull.Visualisation.Core.Persistence.RecentProjects.RecentProject

[<TestFixture>]
type RecentProjectTest () =
    [<Test>]
    member this.``Removing a RecentProject should return a sequence without this RecentProject`` () =
        // Setup
        let recentProject: RecentProject.T = { LastOpened = System.DateTime.Now; Path = "somePath" }
        let otherRecentProject (name: string) : RecentProject.T = { LastOpened = System.DateTime.Now; Path = name }

        let expectedResults = 
            [ otherRecentProject "1"; otherRecentProject "2" ]
            |> List.sortByDescending (fun v -> v.LastOpened)
        let allProjects = 
            expectedResults
            |> List.append [ recentProject ] 
            |> ( List.sortByDescending (fun v -> v.LastOpened) )

        // Call
        let result = (RecentProject.removeRecentProject recentProject allProjects) |> List.ofSeq

        // Assert
        result |> should equal expectedResults

    [<Test>]
    member this.``Updating an existing RecentProject should return a sequence with this RecentProject updated`` () =
        // Setup
        let recentProjectOld: RecentProject.T = { LastOpened = System.DateTime.Today; Path = "somePath" }
        let recentProjectNew: RecentProject.T = { LastOpened = System.DateTime.Now;   Path = "somePath" }

        let otherRecentProject (name: string) : RecentProject.T = { LastOpened = System.DateTime.Now; Path = name }

        let otherRecentProjects = 
            [ otherRecentProject "1"; otherRecentProject "2" ]
            |> List.sortByDescending (fun v -> v.LastOpened)
        
        let initialProjects = 
            otherRecentProjects
            |> List.append [ recentProjectOld ] 
            |> ( List.sortByDescending (fun v -> v.LastOpened) )

        // Call
        let result = (RecentProject.updateRecentProjects recentProjectNew initialProjects) |> List.ofSeq

        // Assert
        let expectedProjects = 
            otherRecentProjects
            |> List.append [ recentProjectNew ] 
            |> ( List.sortByDescending (fun v -> v.LastOpened) )

        result |> should equal expectedProjects
