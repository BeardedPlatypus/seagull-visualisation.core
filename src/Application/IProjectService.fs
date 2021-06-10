namespace Seagull.Visualisation.Core.Application

open PathLib

/// <summary>
/// <see cref="IProjectService"/> defines the interface with which to create
/// and manipulate seagull projects
/// </summary>
[<Interface>]
type public IProjectService =
    /// <summary>
    /// Create a project given a path to the new manifest file location.
    /// </summary>
    /// <param name="manifestPath">The path to the manifest file.</param>
    /// <remarks>
    /// <paramref name="manifestPath"/> is supposed to be a file path.
    /// </remarks>
    abstract member CreateProject : manifestPath:IPath -> unit 