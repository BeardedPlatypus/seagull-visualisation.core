namespace Seagull.Visualisation.Core.Domain

/// <summary>
/// <see cref="Application"/> module defines the logic and data related to the
/// application
/// </summary>
module Application =
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

