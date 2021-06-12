namespace Seagull.Visualisation.Core.Persistence.World

open SixLabors.ImageSharp
open PathLib

open Seagull.Visualisation.Core.Application.World
open Seagull.Visualisation.Core.Domain.World

/// <summary>
/// <see cref="LocalBehaviourImageSharp"/> implements the <see cref="ILocalBehaviour"/>
/// utilising the ImageSharp library to read and write images.
/// </summary>
[<Sealed>]
type public LocalBehaviourImageSharp (sourceRootPath: IPath) =
    let getTexturePath (index: Tile.Index)
                       (imageExtension: string): IPath =
        let imageExtension = 
            if imageExtension.StartsWith(".") then imageExtension 
            else $".{imageExtension}"
        
        sourceRootPath.Join($"{index.ZoomLevel}", $"{index.X}", $"{index.Y}{imageExtension}")

    interface ILocalBehaviour with 
        member this.RetrieveTile (index: Tile.Index) : Tile.Image Option = 
            let texPath = getTexturePath index ".png"

            if texPath.Exists() then 
                System.IO.File.ReadAllBytes (texPath.ToString ()) 
                |> (Tile.Image >> Some)
            else 
                None

        member this.StoreTile (index: Tile.Index) (image: Tile.Image) : unit =
            let texPath = getTexturePath index ".png"

            let parent = texPath.Parent()
            if not (parent.Exists()) then parent.Mkdir(true)

            use img = Image.Load(image.toBytes ())
            img.Save(texPath.ToString())

