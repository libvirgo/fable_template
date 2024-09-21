module Template.Ping

open Fable.Core

[<Erase>]
type Response =
    inherit JS.Object
    abstract send: obj -> unit

[<Erase>]
type Fastify =
    abstract get<'T> : path: string * handler: (obj -> Response -> JS.Promise<'T>) -> unit
    abstract get<'T> : path: string * handler: (unit -> JS.Promise<'T>) -> unit
    abstract listen: {| port: int |} -> unit

[<ImportDefault("fastify")>]
let fastify: {| logger: bool |} -> Fastify = jsNative

let lambda x = fun () -> x
let app = fastify {| logger = true |}

app.get ("/ping", (fun _ _ -> promise { return {| ping = "pong" |} }))
app.get ("/hello", lambda (promise { return "hello world" }))

app.listen {| port = 3000 |}
