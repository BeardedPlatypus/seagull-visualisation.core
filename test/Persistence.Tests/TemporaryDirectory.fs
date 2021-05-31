namespace Seagull.Visualisation.Core.Persistence.Tests

open System
open PathLib

[<Sealed>]
type TemporaryDirectory () =
    let tempDirectoryPath =
        Paths.Create(System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName())
    
    do tempDirectoryPath.Mkdir(true)
    
    let mutable hasDisposed = false
    let removeDirectory () =
        System.IO.Directory.Delete(tempDirectoryPath.ToString(), true)
    
    member this.Path = tempDirectoryPath
    
    interface IDisposable with
        
        member this.Dispose() =
            if not hasDisposed then
                removeDirectory ()
                hasDisposed = true |> ignore
            else
                ()
            