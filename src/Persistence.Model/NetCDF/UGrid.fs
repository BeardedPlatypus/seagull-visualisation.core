namespace Seagull.Visualisation.Core.Persistence.Model.NetCDF

open BeardedPlatypus.Functional.NetCDF
open PathLib

/// <summary>
/// The <see cref="UGrid"/> module provides common methods to
/// query a NetCDF file with UGrid conventions and retrieve
/// the relevant model data.
/// </summary>
module internal UGrid =
    let internal query (path: IPath) (query: IQuery) =
        Service.Query (path.ToString (), query)

