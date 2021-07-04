namespace Seagull.Visualisation.Core.Persistence.Model.NetCDF

open PathLib
open Seagull.Visualisation.Core.Application.Model

/// <summary>
/// <see cref="ModelRepositoryCreationStrategyNetCDF"/> implements the
/// <see cref="IModelRepositoryCreationStrategy"/> for NetCDF files.
/// </summary>
[<Sealed>]
type public ModelRepositoryCreationStrategyNetCDF (coordinateService: ICoordinateSystemTransformationService, 
                                                   projectCoordinateSystem: EPSGCode) =
    let transformCoordinateSystem = coordinateService.Convert projectCoordinateSystem

    interface IModelRepositoryCreationStrategy with
        member this.CanCreateFor (path: IPath) : bool =
            path.IsFile() && path.Extension = ".nc"

        member this.Create(path: IPath): IModelRepository = 
            let query = UGrid.RetrieveMeshesQuery (path, transformCoordinateSystem)
            UGrid.executeQuery path query

            ModelRepositoryNetCDF(query.Meshes1D, query.Meshes2D)
            :> IModelRepository

