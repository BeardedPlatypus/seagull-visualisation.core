namespace Seagull.Visualisation.Core.Persistence.Tests.Projects

open NUnit.Framework
open FsUnit

open Seagull.Visualisation.Core
open Seagull.Visualisation.Core.Persistence
open Seagull.Visualisation.Core.Persistence.Projects
open Seagull.Visualisation.Core.Persistence.Projects.Project
open Seagull.Visualisation.Core.Persistence.Tests

[<TestFixture>]
type ProjectJsonTest () =
    [<Test>]
    member this.``Creating a project should create a correct file on disk.`` () =
        use tempDir = new TemporaryDirectory ()
        let filePath = tempDir.Path.Join "test.seagull"
        
        ProjectJson.createNewProject filePath

        let readResult =
            Utilities.Json.getObject filePath
            |> Utilities.Json.deserialize<Project.T>

        readResult |> should equal { Version = Version.FromDomain Domain.Projects.ProjectVersion.CurrentVersion }


        
