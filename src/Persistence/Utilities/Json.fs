namespace Seagull.Visualisation.Core.Persistence.Utilities

open System.IO
open PathLib

open Newtonsoft.Json
open Newtonsoft.Json.Linq

// TODO: make this async
/// <summary>
/// The <see cref="Json"/> module provides the methods to handle Json files.
/// </summary>
module internal Json =
    let internal query (propertyPath: string) (obj: JToken) =
        obj.SelectToken propertyPath
        
    let internal getObject (path: IPath) : JToken =
        use sr = new StreamReader (path.Open FileMode.Open)
        use jsonReader = new JsonTextReader(sr)
        
        (JToken.ReadFrom jsonReader) 
        
    let internal toObject (obj: 'T) : JToken =
        JToken.FromObject obj
        
    let internal update<'T> (obj: JToken) (path: string) (value: 'T) : unit =
        obj.Replace (JToken.FromObject value)
        
    let internal remove<'T> (obj: JToken) (path: string) : unit =
        (query path obj).Remove()

    let internal deserialize<'T> (obj: JToken) =
        obj.ToObject<'T>()

    let internal writeObj (path: IPath) (obj: JToken) : unit =
        path.Parent().Mkdir true
        
        use sw = new StreamWriter (path.Open FileMode.CreateNew)
        use jsonWriter = new JsonTextWriter(sw)
        
        obj.WriteTo(jsonWriter)