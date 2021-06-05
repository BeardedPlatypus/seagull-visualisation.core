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
    let internal query (propertyPath: string) (obj: JToken) : Option<JToken> =
        match obj.SelectToken propertyPath with 
        | null -> None
        | v    -> Some v
        
    let internal getObject (path: IPath) : JToken =
        use sr = new StreamReader (path.Open FileMode.Open)
        use jsonReader = new JsonTextReader(sr)
        
        (JToken.ReadFrom jsonReader) 
        
    let internal toObject (obj: 'T) : JToken =
        JToken.FromObject obj
        
    let internal update<'T> (obj: JToken) (path: string) (value: 'T) : unit =
        obj.Replace (JToken.FromObject value)

    module internal Util =
        let internal splitLastSection (path: string) : string * string =
            let index = path.LastIndexOf "."
            path.Substring(0, path.Length - index - 1), path.Substring(index)

        let rec internal addRec (obj: JObject) (path: string) (toAdd: JToken) : unit =
            if path.Length > 0 then 
                match query path obj with 
                | Some v -> 
                    (v :?> JObject).Add toAdd
                | None   ->  
                    let newPath, toAddParent = splitLastSection path

                    let newToAdd = JObject()
                    newToAdd.Add(toAddParent, toAdd)

                    addRec obj newPath newToAdd
            else 
                obj.Add toAdd

    let internal add<'T> (obj: JToken) (path: string) (value: 'T) : unit =
        // Assumption: paths do not contain indices of arrays (e.g. SomeValue[0])
        Util.addRec (obj :?> JObject) path (toObject value)
        
    let internal remove<'T> (obj: JToken) (path: string) : unit =
        match query path obj with 
        | Some v -> v.Remove()
        | None   -> ()

    let internal deserialize<'T> (obj: JToken) =
        obj.ToObject<'T>()

    let internal writeObj (path: IPath) (obj: JToken) : unit =
        path.Parent().Mkdir true
        
        use sw = new StreamWriter (path.Open FileMode.CreateNew)
        use jsonWriter = new JsonTextWriter(sw)
        
        obj.WriteTo(jsonWriter)

    let internal emptyObj () : JToken = JObject() :> JToken
