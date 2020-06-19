namespace TicTacToe.IO

open System
open TicTacToe.DomainType
open TicTacToe.Validation

[<AutoOpen>]
module IO =
    let printTable (table: Cell []) =
        table
        |> Seq.splitInto 3
        |> Seq.map (fun nRow ->
            nRow.[0].Symbol
            + " | "
            + nRow.[1].Symbol
            + " | "
            + nRow.[2].Symbol
            + " |\n")
        |> Seq.reduce (+)
        |> printf "%s\n"

    let getPosition () =
        printf "Set Cell position: "
        Console.ReadLine() |> isNotNumberValid
