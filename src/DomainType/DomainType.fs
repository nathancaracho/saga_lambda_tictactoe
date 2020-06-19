namespace TicTacToe.DomainType

[<AutoOpen>]
module DomainType =
    type Cell = { Symbol: string; Position: int }
    type Turn = { Table: Cell []; Position: int }

     let createTable =
        [| for i in 1 .. 9 -> { Symbol = (string i); Position = i } |]
