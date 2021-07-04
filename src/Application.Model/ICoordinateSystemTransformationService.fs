namespace Seagull.Visualisation.Core.Application.Model

/// <summary>
/// <see cref="ICoordinateSystemTransformationService"/> defines the 
/// interface with which to convert coordinates from one coordinate system
/// to another.
/// </summary>
[<Interface>]
type public ICoordinateSystemTransformationService =
    /// <summary>
    /// Convert the specified coordinate with x and y coordinate values
    /// in the <paramref name="fromCoordinateSystem"/> to the corresponding
    /// coordinates in the <paramref name="toCoordinateSystem"/>
    /// </summary>
    /// <param name="toCoordinateSystem">
    /// The coordinate system to which the coordinates should be transformed.
    /// </param>
    /// <param name="fromCoordinateSystem">
    /// The coordinate system from which the coordinates should be transformed.
    /// </param>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The x-coordinate.</param>
    /// <returns>
    /// The coordinates in the <paramref name="toCoordinateSystem"/>
    /// corresponding with the provided coordinates in the 
    /// <paramref name="fromCoordinateSystem"/>.
    /// </returns>
    abstract member Convert: toCoordinateSystem: EPSGCode ->
                             fromCoordinateSystem: EPSGCode ->
                             x: double * y: double -> 
                             double * double

