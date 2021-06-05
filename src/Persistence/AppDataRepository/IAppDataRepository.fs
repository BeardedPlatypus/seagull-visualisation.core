namespace Seagull.Visualisation.Core.Persistence.AppDataRepository

[<Interface>]
type public IAppDataRepository =
    abstract member Query : key:string -> defaultValue:'T -> 'T
    abstract member Write : key:string -> value:'T -> unit
    