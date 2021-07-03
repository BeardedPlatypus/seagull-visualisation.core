namespace Seagull.Visualisation.Core.Application.Model

open PathLib

/// <summary>
/// <see cref="IModelRepositoryCreationStrategy"/> defines the interface with
/// which <see cref="IModelRepository"/> objects can be created. These creation
/// strategies are used registered within the <see cref="ModelRepositoryFactory"/>.
/// </summary>
[<Interface>]
type public IModelRepositoryCreationStrategy =
    /// <summary>
    /// Whether this <see cref="IModelRepositoryCreationStrategy"/>
    /// can create a <see cref="IModelRepository"/> from the provided
    /// <paramref name="path"/>. 
    /// </summary>
    /// <param name="path">
    /// The path to file or directory from which a repository should be created
    /// by this <see cref="IModelRepositoryCreationStrategy"/>.
    /// </param>
    /// <returns>
    /// True if a <see cref="IModelRepository"/> can be created; false otherwise.
    /// </returns>
    /// <remarks>
    /// <paramref name="path"/> is expected to be a valid path which exists on the
    /// file system.
    /// </remarks
    /// <remarks>
    /// This method is expected to be called more than the <see cref="Create"/>
    /// function, as such it should be fast.
    /// </remarks>
    abstract member CanCreateFor: path: IPath -> bool

    /// <summary>
    /// Create a <see cref="IModelRepository"/> from the provided 
    /// <paramref name="path"/>.
    /// </summary>
    /// <param name="path">
    /// The path to file or directory from which a repository is created
    /// by this <see cref="IModelRepositoryCreationStrategy"/>.
    /// </param>
    /// <returns>
    /// The <see cref="IModelRepository"/> corresponding with the provided
    /// <paramref name="path"/>.
    /// </returns>
    /// <remarks>
    /// This method will be called if and only if <see cref="CanCreateFor"/> is
    /// true.
    /// </remarks>
    abstract member Create: path: IPath -> IModelRepository

