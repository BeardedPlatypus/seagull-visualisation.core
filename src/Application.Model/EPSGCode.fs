namespace Seagull.Visualisation.Core.Application.Model

/// <summary>
/// <see cref="EPSGCode"/> defines a single epsg code used to transform 
/// coordinates from coordinate system to another.
/// </summary>
[<Struct>]
type public EPSGCode =
    struct 
        /// <summary>
        /// The ID of this <see cref="EPSGCode"/>.
        /// </summary>
        val ID: int

        /// <summary>
        /// Create a new <see cref="EPSGCode"/> with the given 
        /// <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The ID of the new epsg code</param>
        new(id: int) = { ID = id }
    end

