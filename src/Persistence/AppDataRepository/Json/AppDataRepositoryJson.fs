namespace Seagull.Visualisation.Core.Persistence.AppDataRepository

open PathLib
open Seagull.Visualisation.Core.Persistence.Utilities

type public AppDataRepositoryJson (appDataFolder: IPath) =
    let appDataPath: IPath = appDataFolder.Join ( PurePath.Create "AppData.json" )
    
    interface IAppDataRepository with
        member this.Query<'T>(key: string) =
            try
                Json.getObject appDataPath
                |> ( Json.query key )
                |> ( Json.deserialize<'T> )
                |> Result.Ok
            with
                | ex -> Result.Error ()
            
        member this.Write (key: string) (value: 'T) : unit =
            try 
                let obj = Json.getObject appDataPath
                (Json.query key obj).Replace (Json.toObject value )
                Json.writeObj appDataPath obj
            with
               | ex -> ()
            