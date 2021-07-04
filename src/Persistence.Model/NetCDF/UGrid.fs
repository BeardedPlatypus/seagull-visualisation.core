namespace Seagull.Visualisation.Core.Persistence.Model.NetCDF

open BeardedPlatypus.Functional.NetCDF
open PathLib

/// <summary>
/// The <see cref="UGrid"/> module provides common methods to
/// query a NetCDF file with UGrid conventions and retrieve
/// the relevant model data.
/// </summary>
module internal UGrid =
    let internal query (path: IPath) (query: IQuery) =
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

