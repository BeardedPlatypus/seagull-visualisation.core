namespace Seagull.Visualisation.Core.Persistence.World

open SixLabors.ImageSharp
open PathLib

open Seagull.Visualisation.Core.Application.World
open Seagull.Visualisation.Core.Domain.World

[<Sealed>]
type public LocalTileServiceImageSharp (repositoryRootPath: IPath) =
    let getTexturePath (tileSourceName: string) 
                       (index: Tile.Index)
                       (imageExtension: string): IPath =
        let tileSourceName = tileSourceName.Replace(' ', '_')
        let imageExtension = 
            if imageExtension.StartsWith(".") then imageExtension 
            else $".{imageExtension}"
        
        repositoryRootPath.Join(tileSourceName, $"{index.ZoomLevel}", $"{index.X}", $"{index.Y}{imageExtension}")

    interface ILocalTileService with
        member this.GetLocalTile (source:Tile.Source) (index:Tile.Index) : Tile.Image Option =
            let texPath = getTexturePath (source.ToStringRepr ()) index ".png"

            if texPath.Exists() then 
                System.IO.File.ReadAllBytes (texPath.ToString ()) 
                |> Tile.Image
                |> Some
            else 
                None
                
        member this.StoreLocalTile(source: Tile.Source) (index: Tile.Index) (image: Tile.Image): unit = 
            let texPath = getTexturePath (source.ToStringRepr ()) index ".png"

            let parent = texPath.Parent()
            if not (parent.Exists()) then parent.Mkdir(true)

            use img = Image.Load(image.toBytes ())
            img.Save(texPath.ToString())

