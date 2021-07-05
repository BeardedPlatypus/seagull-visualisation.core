namespace Seagull.Visualisation.Core.Persistence.Model.NetCDF

open BeardedPlatypus.Functional.NetCDF
open PathLib

open Seagull.Visualisation.Core.Domain.Model
open Seagull.Visualisation.Core.Application.Model

/// <summary>
/// The <see cref="UGrid"/> module provides common methods to
/// query a NetCDF file with UGrid conventions and retrieve
/// the relevant model data.
/// </summary>
module internal UGrid =
    let internal executeQuery (path: IPath) (query: IQuery) =
        Service.Query (path.ToString (), query)

    [<Sealed>]
    type internal RetrieveVerticesQuery (meshID: VariableID,
                                         transformCoordinateSystem: EPSGCode -> (double * double) -> (double * double)) = 
        let mutable result : Grid.Vertex2D list = List.empty

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

        let getCoordinateSystem (repository: IRepository) : EPSGCode =
            // Assumption: netcdf files only contain a single coordinate system
            // We assume all files contain such a coordinate system, this is however false
            // this should be replaced with a try get variable attribute when it becomes available.
            let coordinateSystemID = 
                (repository.RetrieveVariableAttribute("projected_coordinate_system", "epsg")).Values
                |> Seq.head

            EPSGCode(coordinateSystemID)

        member this.Result = result |> Seq.ofList

        interface IQuery with
            member this.Execute (repository: IRepository) : unit = 
                let vertexVariableNames = getVertexVariableNames repository
                let coordinateSystem = getCoordinateSystem repository

                let xVertexData = getCoordinateData repository "projection_x_coordinate" vertexVariableNames
                let yVertexData = getCoordinateData repository "projection_y_coordinate" vertexVariableNames

                let toVertex (x: double) (y: double): Grid.Vertex2D =
                    let (actualX: double, actualY: double) = transformCoordinateSystem coordinateSystem (x, y)
                    Grid.Vertex2D(actualX, actualY)

                let vertices = Seq.map2 toVertex xVertexData yVertexData |> List.ofSeq
                result <- vertices

    let private chunkify<'T> (size: int) (elems: 'T seq) : 'T list list = 
        let folder (elem: 'T) (accSmall: 'T list, accTotal: 'T list list ) : 'T list * 'T list list =
            if List.length accSmall = size then 
                ( [ elem ], accSmall :: accTotal )
            else 
                ( elem :: accSmall, accTotal )
        
        let accSmall, accTotal = Seq.foldBack folder elems ([], [])
        accSmall :: accTotal

    [<Sealed>]
    type internal RetrieveEdgesQuery (meshID: VariableID) = 
         let mutable result : Grid.Edge2D list = List.empty

         member this.Result = result |> Seq.ofList

         interface IQuery with 
            member this.Execute (repository: IRepository) : unit = 
                let edgeVariable: string = (repository.RetrieveVariableAttribute (meshID, "edge_node_connectivity")).Values
                                           |> Seq.head
                let edgeData = repository.RetrieveVariableValue<int32>(edgeVariable)
                let startingIndex: int = (repository.RetrieveVariableAttribute(edgeVariable, "start_index")).Values
                                         |> Seq.head

                let edges = 
                    edgeData.Values
                    |> chunkify 2
                    |> Seq.map (fun [a; b] -> Grid.Edge2D(a - startingIndex, b - startingIndex))
                    |> List.ofSeq

                result <- edges 

    [<Sealed>]
    type internal RetrieveFacesQuery (meshID: VariableID) = 
         let mutable result : Grid.Face2D list = List.empty

         member this.Result = result |> Seq.ofList

         interface IQuery with 
            member this.Execute (repository: IRepository) : unit = 
                let faceVariable: string = (repository.RetrieveVariableAttribute (meshID, "face_node_connectivity")).Values
                                           |> Seq.head
                let faceData = repository.RetrieveVariableValue<int32>(faceVariable)
                let startingIndex: int = (repository.RetrieveVariableAttribute(faceVariable, "start_index")).Values
                                         |> Seq.head
                let fillValue: int = (repository.RetrieveVariableAttribute (faceVariable, "_FillValue")).Values
                                     |> Seq.head
                let maxFaceSize: int = faceData.Shape |> Seq.item 1

                let buildFace (indices: int list) : Grid.Face2D =
                     let interpretedIndices =
                         indices 
                         |> List.filter (fun i -> i <> fillValue)
                         |> List.map (fun i -> i - startingIndex)

                     Grid.Face2D(interpretedIndices)

                let faces = 
                    faceData.Values
                    |> chunkify maxFaceSize
                    |> List.map buildFace

                result <- faces 

    [<Sealed>]
    type internal RetrieveMeshesQuery (netCDFPath: IPath,
                                       transformCoordinateSystem: EPSGCode -> (double * double) -> (double * double)) = 
        let getDimensionMesh (repository: IRepository) (meshID: VariableID): int =
            (repository.RetrieveVariableAttribute (meshID, "topology_dimension")).Values
            |> Seq.head

        let retrieveVertices (meshID: VariableID): unit -> seq<Grid.Vertex2D> = 
            fun () -> 
                let query = RetrieveVerticesQuery (meshID, transformCoordinateSystem)
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

        let mutable meshes1D: IMesh1D list = List.empty
        let mutable meshes2D: IMesh2D list = List.empty

        member this.Meshes1D: seq<IMesh1D> = meshes1D |> Seq.ofList
        member this.Meshes2D: seq<IMesh2D> = meshes2D |> Seq.ofList


        interface IQuery with 
            member this.Execute (repository: IRepository) : unit =
                let meshIds : seq<VariableID> = repository.RetrieveVariablesWithAttributeWithValue ("cf_role", "mesh_topology")

                for (dim, ids) in meshIds |> Seq.groupBy (getDimensionMesh repository) do
                    match dim with 
                    | 1 -> 
                        meshes1D <- ( Seq.map (createMesh1D repository) ids ) |> List.ofSeq
                    | 2 -> 
                        meshes2D <- ( Seq.map (createMesh2D repository) ids ) |> List.ofSeq
                    | _ ->
                        ()

