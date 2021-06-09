namespace Seagull.Visualisation.Core.Persistence.AppDataRepository

open PathLib
open Seagull.Visualisation.Core.Persistence.Utilities

[<Sealed>]
type public AppDataRepositoryJson (appDataFolder: IPath) =
    member internal this.appDataPath: IPath = appDataFolder.Join ( PurePath.Create "AppData.json" )

    interface IAppDataRepository with
        member this.Query<'T> (key: string) (defaultValue: 'T) : 'T =
            if this.appDataPath.Exists() then 
                Json.getObject this.appDataPath
                |> ( Json.query key )
                |> Option.map ( Json.deserialize<'T> )
                |> Option.defaultValue defaultValue
            else 
               defaultValue
            
        member this.Write (key: string) (value: 'T) : unit =
            try 
                let obj = 
                    if this.appDataPath.Exists() then Json.getObject this.appDataPath
                    else Json.emptyObj ()

                match Json.query key obj with 
                | Some v -> Json.update obj key value
                | None   -> Json.add obj key value
                
                Json.writeObj this.appDataPath obj
            with
               | _ -> ()
            