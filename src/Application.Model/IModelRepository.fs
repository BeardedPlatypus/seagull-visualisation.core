namespace Seagull.Visualisation.Core.Application.Model

open Seagull.Visualisation.Core.Domain.Model

/// <summary>
/// <see cref="IModelRepository"/> defines the interface with which
/// to query a single model.
/// </summary>
[<Interface>]
type public IModelRepository =
    /// <summary>
    /// Retrieve the one-dimensional meshes from this model.
    /// </summary>
    /// <returns>
    /// A collection of one-dimensional meshes contained in this
    /// <see cref="IModelRepository"/>
    /// </returns>
    abstract member RetrieveMeshes1D: unit -> seq<IMesh1D>

    /// <summary>
    /// Retrieve the two-dimensional meshes from this model.
    /// </summary>
    /// <returns>
    /// A collection of two-dimensional meshes contained in this
    /// <see cref="IModelRepository"/>
    /// </returns>
    abstract member RetrieveMeshes2D: unit -> seq<IMesh2D>

