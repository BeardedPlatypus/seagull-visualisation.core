namespace Seagull.Visualisation.Core.Application

open PathLib
open Seagull.Visualisation.Core.Domain.Projects

[<Interface>]
type public IProjectService =
    abstract member CreateProject : IPath -> unit 