namespace Seagull.Visualisation.Core.Infrastructure.Model

open System.IO
open System.Reflection

open Seagull.Visualisation.Core.Application.Model

[<Sealed>]
type public CoordinateSystemTransformationService() = 
    let createWKT (line: string): EPSGCode * string =
        let parts = line.Split(';')
        (EPSGCode(parts.[0] |> int), parts.[1])

    let coordinateSystemMapping: Map<EPSGCode, string> = 
        let assembly = Assembly.GetExecutingAssembly ()
        let resourceName ="Seagull.Visualisation.Core.Infrastructure.Model.SRID.csv"

        use stream: Stream = assembly.GetManifestResourceStream(resourceName)
        use reader: StreamReader = new StreamReader(stream)

        reader.ReadToEnd().Split('\n')
        |> Array.map createWKT
        |> Map

    let transformationDict: System.Collections.Generic.Dictionary<EPSGCode * EPSGCode, GeoAPI.CoordinateSystems.Transformations.ICoordinateTransformation> =
         System.Collections.Generic.Dictionary()

    let createTransformation (toCoordSystem: EPSGCode) (fromCoordSystem: EPSGCode) =
         let coordFactory = ProjNet.CoordinateSystems.CoordinateSystemFactory()

         let toCoordinateSystem =
             coordFactory.CreateFromWkt(coordinateSystemMapping.[toCoordSystem])

         let fromCoordinateSystem =
             coordFactory.CreateFromWkt(coordinateSystemMapping.[fromCoordSystem])

         let transFactory = ProjNet.CoordinateSystems.Transformations.CoordinateTransformationFactory()
         transFactory.CreateFromCoordinateSystems(fromCoordinateSystem, toCoordinateSystem)

    let getTransformation (toCoordSystem: EPSGCode) (fromCoordSystem: EPSGCode): GeoAPI.CoordinateSystems.Transformations.ICoordinateTransformation =
        match transformationDict.TryGetValue((toCoordSystem, fromCoordSystem)) with 
        | true, value -> 
            value
        | _           ->
            let transformation = createTransformation toCoordSystem fromCoordSystem
            transformationDict.Add((toCoordSystem, fromCoordSystem), transformation)
            transformation

    interface ICoordinateSystemTransformationService with
        member this.Convert (toCoordinateSystem: EPSGCode)
                            (fromCoordinateSystem: EPSGCode)
                            (x: double, y: double):
                            double * double =
            if (coordinateSystemMapping.ContainsKey toCoordinateSystem &&
                coordinateSystemMapping.ContainsKey fromCoordinateSystem) then
                let transformation = getTransformation toCoordinateSystem fromCoordinateSystem
                let values: double[] = transformation.MathTransform.Transform([| x; y |])
                (values.[0], values.[1])
            else
                (x, y)
            
