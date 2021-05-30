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
    let internal query (propertyPath: string) (obj: JObject) =
        obj.SelectToken propertyPath
        
    let internal getObject (path: IPath) =
        use sr = new StreamReader (path.Open FileMode.Open)
        use jsonReader = new JsonTextReader(sr)
        
        (JToken.ReadFrom jsonReader) :?> JObject
        
    let internal toObject (obj: 'T) : JObject =
        JObject.FromObject obj
        
    let internal update<'T> (obj: JObject) (path: string) (value: 'T) : unit =
        obj.Replace (JToken.FromObject value)
        
    let internal remove<'T> (obj: JObject) (path: string) : unit =
        (query path obj).Remove()

    let internal deserialize<'T> (obj: JToken) =
        obj.ToObject<'T>()

    let internal writeObj (path: IPath) (obj: JObject) : unit =
        path.Parent().Mkdir true
        
        use sw = new StreamWriter (path.Open FileMode.CreateNew)
        use jsonWriter = new JsonTextWriter(sw)
        
        obj.WriteTo(jsonWriter)