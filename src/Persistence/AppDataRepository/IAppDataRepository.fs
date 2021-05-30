namespace Seagull.Visualisation.Core.Persistence.AppDataRepository

[<Interface>]
type public IAppDataRepository =
    abstract member Query : key:string -> Result<'T, unit>
    abstract member Write : key:string -> value:'T -> unit
    