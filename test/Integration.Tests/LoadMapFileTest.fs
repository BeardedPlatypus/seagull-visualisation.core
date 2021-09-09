namespace Seagull.Visualisation.Core.Integration.Tests

open NUnit.Framework
open FsUnit
open PathLib

open Seagull.Visualisation.Core.Application.Model
open Seagull.Visualisation.Core.Persistence.Model
open Seagull.Visualisation.Core.Infrastructure.Model

[<TestFixture>]
type LoadMapFileTest () =
    [<Test>]
    member this.``Loading map.nc through the ModelRepositoryFactory produces a correct ModelRepository.`` () =
        // Setup
        let transformationService = CoordinateSystemTransformationService()
        let netcdfStrategy = NetCDF.ModelRepositoryCreationStrategyNetCDF(transformationService,
                                                                          EPSGCode(3857))
        let factory = ModelRepositoryFactory(seq { netcdfStrategy :> IModelRepositoryCreationStrategy })

        let modelPath: IPath = Paths.Create("test-data/map.nc")

        factory.CanCreateFor modelPath |> should be True

        let modelRepository = factory.Create modelPath

        modelRepository.RetrieveMeshes1D () |> should be Empty
        modelRepository.RetrieveMeshes2D () |> Seq.length |> should equal 1

        let mesh2D = modelRepository.RetrieveMeshes2D () |> Seq.head

        mesh2D.RetrieveName () |> should equal "mesh2d"

        let vertices = mesh2D.RetrieveVertices () |> List.ofSeq
        vertices |> List.length |> should equal 36

        let edges = mesh2D.RetrieveEdges () |> List.ofSeq
        edges |> List.length |> should equal 60

        let faces = mesh2D.RetrieveFaces () |> List.ofSeq
        faces |> List.length |> should equal 25

