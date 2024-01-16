namespace TurboCart.Infrastructure.Networking.SignalR.Hubs

open TurboCart.Infrastructure.Networking.SignalR.Interfaces
open System.Threading.Tasks
open TurboCart.Domain.Entities
open Microsoft.AspNetCore.SignalR

type SessionHub () =
    inherit Hub<ISessionClient> ()

    static let mutable lastSesion : Option<TurboCartSession> = None

    member this.SessionChanged (session : TurboCartSession) : Task =
        lastSesion <- Some session

        this.Clients.Others.ReceiveSession session

    override this.OnConnectedAsync () : Task =
        match lastSesion with
        | Some s -> this.Clients.Caller.ReceiveSession s
        | None -> Task.CompletedTask