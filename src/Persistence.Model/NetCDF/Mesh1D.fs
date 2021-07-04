namespace Seagull.Visualisation.Core.Persistence.Model.NetCDF

open Seagull.Visualisation.Core.Domain.Model

[<Sealed>]
type internal Mesh1D (name: string, 
                      queryVertices: unit -> seq<Grid.Vertex2D>,
                      queryEdges: unit -> seq<Grid.Edge2D>) =
    interface IMesh1D with
        member this.RetrieveName (): string = name
 
        member this.RetrieveVertices(): seq<Grid.Vertex2D> = 
            queryVertices ()
 
        member this.RetrieveEdges(): seq<Grid.Edge2D> = 
            queryEdges()

