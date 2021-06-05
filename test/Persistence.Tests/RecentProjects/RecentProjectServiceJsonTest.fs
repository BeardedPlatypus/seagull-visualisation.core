namespace Seagull.Visualisation.Core.Persistence.Tests.RecentProjects

open NUnit.Framework
open FsUnit

open PathLib

open Seagull.Visualisation.Core
open Seagull.Visualisation.Core.Application
open Seagull.Visualisation.Core.Persistence.RecentProjects
open Seagull.Visualisation.Core.Persistence.AppDataRepository
open Seagull.Visualisation.Core.Persistence.Utilities
open Seagull.Visualisation.Core.Persistence.Tests

[<TestFixture>]
type RecentProjectServiceJsonTest () =
    let exampleJsonString = "{\"RecentProjects\":[{\"LastOpened\":\"2008-04-05T07:00:00\",\"Path\":\"5.seagull\"},{\"LastOpened\":\"2008-04-04T07:00:00\",\"Path\":\"4.seagull\"},{\"LastOpened\":\"2008-03-03T07:00:00\",\"Path\":\"3.seagull\"},{\"LastOpened\":\"2008-03-02T07:00:00\",\"Path\":\"2.seagull\"},{\"LastOpened\":\"2008-03-01T07:00:00\",\"Path\":\"1.seagull\"}]}"
    let emptyJsonString = "{}"
    let irrelevantDataJsonString = "{ \"SomeData\":[] }"

    let writeTo (line: string) (path: IPath) : unit =
        use sw = new System.IO.StreamWriter(path.Open System.IO.FileMode.Create)
        sw.WriteLine line
        sw.Close()

    let assertCorrectRecentProject (sut: Domain.RecentProject) (expected: Domain.RecentProject) : unit =
        sut.Path.ToString() |> should equal (expected.Path.ToString())
        sut.LastOpened |> should equal expected.LastOpened

    [<Test>]
    member this.``When reading an existent appdata with recent projects, RecentProjectServiceJson should return the correct data.`` () =
        // Setup
        use tempDir = new TemporaryDirectory()

        let appData = AppDataRepositoryJson(tempDir.Path)
        ( exampleJsonString |> writeTo ) appData.appDataPath

        let service = RecentProjectServiceJson(appData) :> IRecentProjectsService


        // Call
        let result = service.GetRecentProjects () |> List.ofSeq

        // Assert
        result |> should haveLength 5

        let recentProject0 = Domain.RecentProject(Paths.Create("5.seagull"), System.DateTime(2008, 4, 5, 7, 0, 0))
        assertCorrectRecentProject result.[0] recentProject0

        let recentProject1 = Domain.RecentProject(Paths.Create("4.seagull"), System.DateTime(2008, 4, 4, 7, 0, 0))
        assertCorrectRecentProject result.[1] recentProject1

        let recentProject2 = Domain.RecentProject(Paths.Create("3.seagull"), System.DateTime(2008, 3, 3, 7, 0, 0))
        assertCorrectRecentProject result.[2] recentProject2

        let recentProject3 = Domain.RecentProject(Paths.Create("2.seagull"), System.DateTime(2008, 3, 2, 7, 0, 0))
        assertCorrectRecentProject result.[3] recentProject3

        let recentProject4 = Domain.RecentProject(Paths.Create("1.seagull"), System.DateTime(2008, 3, 1, 7, 0, 0))
        assertCorrectRecentProject result.[4] recentProject4

    [<Test>]
    member this.``When writing to a non-existent appdata, RecentProjectServiceJson should create a new file with the correct data.`` () =
        // Setup
        use tempDir = new TemporaryDirectory()

        let appData = AppDataRepositoryJson(tempDir.Path)
        let service = RecentProjectServiceJson(appData) :> IRecentProjectsService

        let recentProject = Domain.RecentProject(tempDir.Path.Join "someRecentFile", System.DateTime.Now )

        // Call
        service.UpdateRecentProject recentProject
        let result = service.GetRecentProjects ()


        // Assert
        result |> should haveCount 1
        
        assertCorrectRecentProject (Seq.head result) recentProject

    [<Test>]
    member this.``When writing to an empty appdata, RecentProjectServiceJson should add the correct data.`` () =
        // Setup
        use tempDir = new TemporaryDirectory()

        let appData = AppDataRepositoryJson(tempDir.Path)
        ( emptyJsonString |> writeTo ) appData.appDataPath

        let service = RecentProjectServiceJson(appData) :> IRecentProjectsService

        let recentProject = Domain.RecentProject(tempDir.Path.Join "someRecentFile", System.DateTime.Now )

        // Call
        service.UpdateRecentProject recentProject
        let result = service.GetRecentProjects ()


        // Assert
        result |> should haveCount 1
        
        assertCorrectRecentProject (Seq.head result) recentProject

    [<Test>]
    member this.``When writing to an appdata with other data, RecentProjectServiceJson should add the correct data.`` () =
        // Setup
        use tempDir = new TemporaryDirectory()

        let appData = AppDataRepositoryJson(tempDir.Path)
        ( irrelevantDataJsonString |> writeTo ) appData.appDataPath

        let service = RecentProjectServiceJson(appData) :> IRecentProjectsService

        let recentProject = Domain.RecentProject(tempDir.Path.Join "someRecentFile", System.DateTime.Now )

        // Call
        service.UpdateRecentProject recentProject
        let result = service.GetRecentProjects ()


        // Assert
        result |> should haveCount 1
        
        assertCorrectRecentProject (Seq.head result) recentProject

        (Json.getObject appData.appDataPath |> Json.query "SomeData") |> should not' (be null)

    [<Test>]
    member this.``When writing to an existent appdata, RecentProjectServiceJson should add the correct data.`` () =
        // Setup
        use tempDir = new TemporaryDirectory()

        let appData = AppDataRepositoryJson(tempDir.Path)
        ( exampleJsonString |> writeTo ) appData.appDataPath

        let service = RecentProjectServiceJson(appData) :> IRecentProjectsService

        let recentProjectNew = Domain.RecentProject(tempDir.Path.Join "someRecentFile", System.DateTime.Now )

        // Call
        service.UpdateRecentProject recentProjectNew
        let result = service.GetRecentProjects () |> List.ofSeq

        // Assert
        result |> should haveLength 6

        assertCorrectRecentProject result.[0] recentProjectNew

        let recentProject0 = Domain.RecentProject(Paths.Create("5.seagull"), System.DateTime(2008, 4, 5, 7, 0, 0))
        assertCorrectRecentProject result.[1] recentProject0

        let recentProject1 = Domain.RecentProject(Paths.Create("4.seagull"), System.DateTime(2008, 4, 4, 7, 0, 0))
        assertCorrectRecentProject result.[2] recentProject1

        let recentProject2 = Domain.RecentProject(Paths.Create("3.seagull"), System.DateTime(2008, 3, 3, 7, 0, 0))
        assertCorrectRecentProject result.[3] recentProject2

        let recentProject3 = Domain.RecentProject(Paths.Create("2.seagull"), System.DateTime(2008, 3, 2, 7, 0, 0))
        assertCorrectRecentProject result.[4] recentProject3

        let recentProject4 = Domain.RecentProject(Paths.Create("1.seagull"), System.DateTime(2008, 3, 1, 7, 0, 0))
        assertCorrectRecentProject result.[5] recentProject4

    [<Test>]
    member this.``When updating an existing RecentProject, RecentProjectServiceJson should replace the correct data.`` () =
        // Setup
        use tempDir = new TemporaryDirectory()

        let appData = AppDataRepositoryJson(tempDir.Path)
        ( exampleJsonString |> writeTo ) appData.appDataPath

        let service = RecentProjectServiceJson(appData) :> IRecentProjectsService

        let recentProjectNew = Domain.RecentProject(Paths.Create "2.seagull", System.DateTime.Now )

        // Call
        service.UpdateRecentProject recentProjectNew
        let result = service.GetRecentProjects () |> List.ofSeq

        // Assert
        result |> should haveLength 5

        assertCorrectRecentProject result.[0] recentProjectNew

        let recentProject0 = Domain.RecentProject(Paths.Create("5.seagull"), System.DateTime(2008, 4, 5, 7, 0, 0))
        assertCorrectRecentProject result.[1] recentProject0

        let recentProject1 = Domain.RecentProject(Paths.Create("4.seagull"), System.DateTime(2008, 4, 4, 7, 0, 0))
        assertCorrectRecentProject result.[2] recentProject1

        let recentProject2 = Domain.RecentProject(Paths.Create("3.seagull"), System.DateTime(2008, 3, 3, 7, 0, 0))
        assertCorrectRecentProject result.[3] recentProject2

        let recentProject4 = Domain.RecentProject(Paths.Create("1.seagull"), System.DateTime(2008, 3, 1, 7, 0, 0))
        assertCorrectRecentProject result.[4] recentProject4
