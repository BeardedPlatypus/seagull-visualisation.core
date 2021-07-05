namespace Seagull.Visualisation.Core.Persistence.Model.NetCDF

open Seagull.Visualisation.Core.Domain.Model

[<Sealed>]
type internal Mesh2D (name: string, 
                      queryVertices: unit -> seq<Grid.Vertex2D>,
                      queryEdges: unit -> seq<Grid.Edge2D>,
                      queryFaces: unit -> seq<Grid.Face2D>) = 
    interface IMesh2D with
        member this.RetrieveName (): string = name
 
        member this.RetrieveVertices (): seq<Grid.Vertex2D> = 
            queryVertices ()
 
        member this.RetrieveEdges (): seq<Grid.Edge2D> = 
            queryEdges ()
 
        member this.RetrieveFaces (): seq<Grid.Face2D> = 
            queryFaces ()

