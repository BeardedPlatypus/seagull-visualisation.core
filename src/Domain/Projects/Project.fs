namespace Seagull.Visualisation.Core.Domain.Projects

open PathLib

type Project(version: ProjectVersion,
             projectManifestPath: IPurePath) =
    let projectManifestPath = projectManifestPath
    
    member this.Version = version
    