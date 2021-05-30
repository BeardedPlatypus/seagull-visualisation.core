namespace Seagull.Visualisation.Core.Domain

open PathLib

/// <summary>
/// The <see cref="Project"/> module defines all domain specific logic
/// related to projects.
/// </summary>
module internal Project =
    /// <summary>
    /// <see cref="Metadata"/> module defines the metadata of a project
    /// as well as related concepts.
    /// </summary>
    module public Metadata =
        /// <summary>
        /// <see cref="Version"/> defines the version of a project
        /// as Major Minor Patch.
        /// </summary>
        /// <remarks>
        /// The project versioning should follow semantic versioning.
        /// </remarks>
        type Version =
            { Major: uint
              Minor: uint
              Patch: uint }
        
        /// <summary>
        /// <see cref="Metadata.T"/> defines the metadata of a project.
        /// </summary>
        [<RequireQualifiedAccess>]
        type T =
            { Version: Version }
            
    /// <summary>
    /// <see cref="Project.Data"/> defines the immutable data of a project.
    /// </summary>
    [<RequireQualifiedAccess>]
    type Data = 
        { Metadata: Metadata.T
          ProjectDirectory: IPurePath }
        
    /// <summary>
    /// <see cref="Project.Id"/> defines the unique id of a project.
    /// </summary>
    [<RequireQualifiedAccess>]
    type Id = | Id of uint
            
    /// <summary>
    /// <see cref="Project.T"/> defines a project.
    /// </summary>
    [<RequireQualifiedAccess>]
    type T =
        { Id: Id
          Data: Data }
