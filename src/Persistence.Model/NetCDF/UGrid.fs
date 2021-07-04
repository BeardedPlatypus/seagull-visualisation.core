namespace Seagull.Visualisation.Core.Persistence.Model.NetCDF

open BeardedPlatypus.Functional.NetCDF
open PathLib

open Seagull.Visualisation.Core.Domain.Model

/// <summary>
/// The <see cref="UGrid"/> module provides common methods to
/// query a NetCDF file with UGrid conventions and retrieve
/// the relevant model data.
/// </summary>
module internal UGrid =
    let internal executeQuery (path: IPath) (query: IQuery) =
        Service.Query (path.ToString (), query)

    [<Sealed>]
    type internal RetrieveVerticesQuery (meshID: VariableID) = 
        let mutable result : seq<Grid.Vertex2D> = Seq.empty

        let getVertexVariableNames (repository: IRepository) : string[] =
            let vertexVariableAttribute: string = 
                (repository.RetrieveVariableAttribute<string>(meshID, "node_coordinates")).Values
                |> Seq.head

            vertexVariableAttribute.Split([|" "|], System.StringSplitOptions.RemoveEmptyEntries)

        let getStandardName (repository: IRepository) (variableName: string) =
            (repository.RetrieveVariableAttribute(variableName, "standard_name")).Values
            |> Seq.head

        let getCoordinateData (repository: IRepository) (coordinateIdentifier: string) (variableNames: string[]): seq<double> =
            let isCoordinateData (variableName: string): bool =
                (getStandardName repository variableName) = coordinateIdentifier

            let coordinateVariableName = Array.find isCoordinateData variableNames
            (repository.RetrieveVariableValue(coordinateVariableName)).Values

        member val Result = result with get

        interface IQuery with
            member this.Execute (repository: IRepository) : unit = 
                let vertexVariableNames = getVertexVariableNames repository

                let xVertexData = getCoordinateData repository "projection_x_coordinate" vertexVariableNames
                let yVertexData = getCoordinateData repository "projection_y_coordinate" vertexVariableNames

                // TODO add conversion here
                let vertices = Seq.map2 (fun x y -> Grid.Vertex2D(x, y)) xVertexData yVertexData
                result <- vertices

    [<Sealed>]
    type internal RetrieveEdgesQuery (meshID: VariableID) = 
         let mutable result : seq<Grid.Edge2D> = Seq.empty

         member val Result = result with get

         interface IQuery with 
            member this.Execute (repository: IRepository) : unit = 
                let edgeVariable: string = (repository.RetrieveVariableAttribute (meshID, "edge_node_connectivity")).Values
                                           |> Seq.head
                let edgeData = repository.RetrieveVariableValue(edgeVariable)
                let startingIndex: int = (repository.RetrieveVariableAttribute(edgeVariable, "start_index")).Values
                                         |> Seq.head

                let edges = 
                    edgeData.Values
                    |> Seq.pairwise
                    |> Seq.map (fun (a, b) -> Grid.Edge2D(a - startingIndex, b - startingIndex))

                result <- edges

    [<Sealed>]
    type internal RetrieveFacesQuery (meshID: VariableID) = 
         let mutable result : seq<Grid.Face2D> = Seq.empty

         member val Result = result with get

         interface IQuery with 
            member this.Execute (repository: IRepository) : unit = 
                let faceVariable: string = (repository.RetrieveVariableAttribute (meshID, "face_node_connectivity")).Values
                                           |> Seq.head
                let faceData = repository.RetrieveVariableValue(faceVariable)
                let startingIndex: int = (repository.RetrieveVariableAttribute(faceVariable, "start_index")).Values
                                         |> Seq.head
                let fillValue: int = (repository.RetrieveVariableAttribute (meshID, "_FillValue")).Values
                                     |> Seq.head
                let maxFaceSize: int = faceData.Shape |> Seq.item 1

                let buildFace (indices: seq<int>) : Grid.Face2D =
                     let interpretedIndices =
                         indices 
                         |> Seq.filter (fun i -> i <> fillValue)
                         |> Seq.map (fun i -> i - startingIndex)

                     Grid.Face2D(interpretedIndices)

                let faces = 
                    faceData.Values
                    |> Seq.windowed maxFaceSize
                    |> Seq.map buildFace

                result <- faces

    [<Sealed>]
    type internal RetrieveMeshesQuery (netCDFPath: IPath) = 
        let getDimensionMesh (repository: IRepository) (meshID: VariableID): int =
            (repository.RetrieveVariableAttribute (meshID, "topology_dimension")).Values
            |> Seq.head

        let retrieveVertices (meshID: VariableID): unit -> seq<Grid.Vertex2D> = 
            fun () -> 
                let query = RetrieveVerticesQuery meshID
                executeQuery netCDFPath query
                query.Result

        let retrieveEdges (meshID: VariableID): unit -> seq<Grid.Edge2D> =
            fun () ->
                let query = RetrieveEdgesQuery meshID
                executeQuery netCDFPath query 
                query.Result

        let retrieveFaces (meshID: VariableID): unit -> seq<Grid.Face2D> =
            fun () ->
                let query = RetrieveFacesQuery meshID
                executeQuery netCDFPath query 
                query.Result

        let createMesh1D (repository: IRepository) (meshID: VariableID): IMesh1D =
            let name = repository.RetrieveVariableName meshID
            Mesh1D(name, retrieveVertices meshID, retrieveEdges meshID)
            :> IMesh1D

        let createMesh2D (repository: IRepository) (meshID: VariableID): IMesh2D =
            let name = repository.RetrieveVariableName meshID
            Mesh2D(name, retrieveVertices meshID, retrieveEdges meshID, retrieveFaces meshID)
            :> IMesh2D

        let mutable meshes1D: seq<IMesh1D> = Seq.empty
        let mutable meshes2D: seq<IMesh2D> = Seq.empty

        member val Meshes1D: seq<IMesh1D> = meshes1D with get
        member val Meshes2D: seq<IMesh2D> = meshes2D with get


        interface IQuery with 
            member this.Execute (repository: IRepository) : unit =
                let meshIds : seq<VariableID> = repository.RetrieveVariablesWithAttributeWithValue ("cf_role", "mesh_topology")

                for (dim, ids) in meshIds |> Seq.groupBy (getDimensionMesh repository) do
                    match dim with 
                    | 1 -> 
                        meshes1D <- Seq.map (createMesh1D repository) ids
                    | 2 -> 
                        meshes2D <- Seq.map (createMesh2D repository) ids
                    | _ ->
                        ()

