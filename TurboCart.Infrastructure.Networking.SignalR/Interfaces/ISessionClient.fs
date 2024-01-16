namespace TurboCart.Infrastructure.Networking.SignalR.Interfaces

open System.Threading.Tasks
open TurboCart.Domain.Entities

type ISessionClient =
    abstract member ReceiveSession : TurboCartSession -> Task