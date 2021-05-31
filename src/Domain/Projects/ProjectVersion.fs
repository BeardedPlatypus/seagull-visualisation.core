namespace Seagull.Visualisation.Core.Domain.Projects

[<Struct>]
type ProjectVersion(major: int,
                    minor: int,
                    patch: int) =
    member this.Major : int = major
    member this.Minor : int = minor
    member this.Patch : int = patch
    
    static member CurrentVersion = ProjectVersion(0, 1, 0)
    