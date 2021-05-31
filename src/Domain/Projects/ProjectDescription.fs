namespace Seagull.Visualisation.Core.Domain.Projects

open PathLib

type ProjectDescription (filePath: IPath,
                         version: ProjectVersion) =
    member this.FilePath = filePath
    member this.Version = version
    