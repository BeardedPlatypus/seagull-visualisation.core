namespace Seagull.Visualisation.Core.Domain.World

/// <summary>
/// The <see cref="Tile"/> module defines all concepts related to working
/// with tiles.
/// </summary>
[<RequireQualifiedAccess>]
module public Tile = 
    /// <summary>
    /// <see cref="Index"/> defines the index of a tile within a tile pyramid.
    /// </summary>
    [<RequireQualifiedAccess>]
    [<Struct>]
    type public Index = 
        { X: int 
          Y: int 
          ZoomLevel: int }

    /// <summary>
    /// <see cref="Image"/> defines an image as used within the seagull 
    /// application.
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

    /// <summary>
    /// <see cref="Source"/> defines an abstract tile image source.
    /// </summary>
    type public Source =
        | Source of string
        member x.ToStringRepr () = match x with | Source s -> s

