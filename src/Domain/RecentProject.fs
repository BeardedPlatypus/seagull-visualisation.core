namespace Seagull.Visualisation.Core.Domain

open System
open PathLib

/// <summary>
/// <see cref="RecentProject"/> defines a recent project consisting of a path
/// and a last time it was opened.
/// </summary>
type public RecentProject (path: IPurePath,
                           lastOpened : DateTime) =
    /// <summary>
    /// The path to the project directory.
    /// </summary>
    member this.Path : IPurePath = path
    
    /// <summary>
    /// The time it was last opened
    /// </summary>
    member this.LastOpened : DateTime = lastOpened