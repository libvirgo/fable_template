module Template.Ping

open Fable.Core

[<Erase>]
type Response =
    inherit JS.Object
    abstract send: obj -> unit

[<Erase>]
type Fastify =
    abstract get<'T> : string -> (obj -> Response -> JS.Promise<'T>) -> unit
    abstract listen: {| port: int |} -> unit

[<ImportDefault("fastify")>]
let fastify: {| logger: bool |} -> Fastify = jsNative

let app = fastify {| logger = true |}

app.get "/" (fun _ _ -> (promise { return {| ping = "pong" |} }))
app.listen {| port = 3000 |}
