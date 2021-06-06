namespace Seagull.Visualisation.Core.Domain.World

/// <summary>
/// The <see cref="Tile"/> module defines all concepts related to working
/// with tiles.
/// </summary>
[<RequireQualifiedAccess>]
module public Tile = 
    [<RequireQualifiedAccess>]
    [<Struct>]
    type public Index = 
        { X: int 
          Y: int 
          ZoomLevel: int }

    /// <summary>
    /// <see cref="Image"/> defines an image as used within the TileSource
    /// library.
    /// </summary>
    type public Image = 
        | Image of byte[]
    
        /// <summary>
        /// Retrieve the underlying bytes array 
        /// </summary>
        /// <returns>
        /// The bytes array corresponding with this <see cref="Image"/>.
        /// </returns>
        member self.toBytes () = match self with | Image b -> b

    type public Source =
        | Source of string
        member x.ToStringRepr () = match x with | Source s -> s

