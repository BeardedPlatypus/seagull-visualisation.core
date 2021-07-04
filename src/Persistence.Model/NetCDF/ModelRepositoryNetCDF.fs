namespace Seagull.Visualisation.Core.Persistence.Model.NetCDF

open Seagull.Visualisation.Core.Application.Model
open Seagull.Visualisation.Core.Domain.Model

[<Sealed>]
type internal ModelRepositoryNetCDF (meshes1D: seq<IMesh1D>,
                                     meshes2D: seq<IMesh2D>) =
    let meshes1D : IMesh1D list = meshes1D |> List.ofSeq
    let meshes2D : IMesh2D list = meshes2D |> List.ofSeq

    interface IModelRepository with 
        member this.RetrieveMeshes1D () =
            meshes1D |> Seq.ofList

        member this.RetrieveMeshes2D () =
            meshes2D |> Seq.ofList
     
