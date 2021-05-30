namespace Seagull.Visualisation.Core.Common

open PathLib

/// <summary>
/// Provides some helper methods in addition to the PathLib library
/// to ease development.
/// </summary>
module public Path =
    let inline public ( / ) (left: IPurePath, right: IPurePath) =
        left.Join right
