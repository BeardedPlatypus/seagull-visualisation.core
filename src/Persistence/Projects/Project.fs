namespace Seagull.Visualisation.Core.Persistence.Projects

open Seagull.Visualisation.Core

module public Project =
    type Version =
        { Major : int
          Minor : int
          Patch : int }
        static member FromDomain (version: Domain.Projects.ProjectVersion): Version =
            { Major = version.Major
              Minor = version.Minor
              Patch = version.Patch }
    
    type T =
        { Version : Version }
        
