namespace Seagull.Visualisation.Core.Domain.Model.Grid

/// <summary>
/// <see cref="Extents"/> defines a rectangle with a min and max value in both
/// the x and y directions.
/// </summary>
/// <remarks>
/// It is expected that xMin is smaller or equal to xMax and yMin is smaller 
/// or equal to yMax.
/// </remarks>
[<Sealed>]
type public Extents (xMin: double, xMax: double, yMin: double, yMax: double) =
    /// <summary>
    /// Gets the minimum x value.
    /// </summary>
    member val public XMin: double = xMin;

    /// <summary>
    /// Gets the maximum x value.
    /// </summary>
    member val public XMax: double = xMax;

    /// <summary>
    /// Gets the minimum y value.
    /// </summary>
    member val public YMin: double = yMin;

    /// <summary>
    /// Gets the maximum y value.
    /// </summary>
    member val public YMax: double = yMax;

