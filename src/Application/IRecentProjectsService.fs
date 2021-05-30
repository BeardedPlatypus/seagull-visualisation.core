namespace Seagull.Visualisation.Core.Application

open Seagull.Visualisation.Core.Domain

/// <summary>
/// <see cref="RecentProjectsService"/> implements the logic related to retrieving
/// and updating the recent projects.
/// </summary>
[<Interface>]
type public IRecentProjectsService =
    /// <summary>
    /// Get the collection of <see cref="RecentProject"/> objects currently
    /// stored in this application.
    /// </summary>
    /// <returns>
    /// The collection of <see cref="RecentProject"/> objects currently stored
    /// in this application.
    /// </returns>
    abstract GetRecentProjects: unit -> seq<RecentProject>
        
    /// <summary>
    /// Update the specified <paramref name="recentProject"/>.
    /// </summary>
    /// <param name="recentProject">The recent project to update.</param>
    /// <remarks>
    /// Recent projects are filtered based on their absolute path. If a
    /// path already exists within the recent projects it is updated, otherwise
    /// it is added.
    /// </remarks>
    abstract UpdateRecentProject: recentProject:RecentProject -> unit
        
    /// <summary>
    /// Remove the specified <paramref name="recentProject"/> from the
    /// recent projects.
    /// </summary>
    /// <param name="recentProject">The recent project to remove.</param>
    /// <remarks>
    /// Recent projects are filtered based on their absolute path. If a
    /// path exists within the recent projects it is removed, otherwise
    /// nothing will occur.
    /// </remarks>
    abstract RemoveRecentProject: recentProject:RecentProject -> unit
