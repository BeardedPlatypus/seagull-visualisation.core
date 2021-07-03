namespace Seagull.Visualisation.Core.Application.Model

open PathLib

/// <summary>
/// <see cref="ModelRepositoryFactory"/> is responsible for creating
/// <see cref="IModelRepository"/> using the provided 
/// <see cref="IModelRepositoryCreationStrategy"/> objects.
/// </summary>
[<Sealed>]
type public ModelRepositoryFactory (strategies: seq<IModelRepositoryCreationStrategy>) =
    let strategies: IModelRepositoryCreationStrategy list = strategies |> List.ofSeq

    let canCreateFor (path: IPath) (strategy: IModelRepositoryCreationStrategy) =
        strategy.CanCreateFor path

    /// <summary>
    /// Whether a <see cref="IModelRepository"/> can be created from the 
    /// provided <paramref name="path"/>.
    /// </summary>
    /// <param name="path">
    /// The path to file or directory from which a repository should be created
    /// by this <see cref="IModelRepositoryFactory"/>.
    /// </param>
    /// <returns>
    /// True if a <see cref="IModelRepository"/> can be created; false otherwise.
    /// </returns>
    /// <remarks>
    /// <paramref name="path"/> is expected to be a valid path which exists on the
    /// file system.
    /// </remarks
    member this.CanCreateFor (path: IPath): bool = 
        strategies |> List.exists (canCreateFor path)

    /// <summary>
    /// Create a <see cref="IModelRepository"/> from the provided 
    /// <paramref name="path"/>.
    /// </summary>
    /// <param name="path">
    /// The path to file or directory from which a repository is created
    /// by this <see cref="IModelRepositoryFactory"/>.
    /// </param>
    /// <returns>
    /// The <see cref="IModelRepository"/> corresponding with the provided
    /// <paramref name="path"/>.
    /// </returns>
    /// <remarks>
    /// This method should be called if and only if <see cref="CanCreateFor"/> is
    /// true.
    /// </remarks>
    member this.Create(path: IPath): IModelRepository =
        let strategy = strategies |> List.find (canCreateFor path)
        strategy.Create path
        
