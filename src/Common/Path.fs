namespace Seagull.Visualisation.Core.Common

open PathLib

/// <summary>
/// Provides some helper methods in addition to the PathLib library
/// to ease development.
/// </summary>
module Path =
    let public (/) (left: IPurePath, right: IPurePath) =
        left.Join right

