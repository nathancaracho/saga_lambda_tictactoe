namespace TicTacToe

open System
open System.Text.RegularExpressions

type Cell = { Symbol: string; Position: int }
type Turn = { Table: Cell []; Position: int }

type GameState<'TSuccess> =
    | Success of 'TSuccess
    | Failure of string
    | Won

module Turn =

    let bind result func =
        match result with
        | Success sResult -> func sResult
        | Failure fResult -> Failure fResult

    let (>>=) result func = bind result func

    let createTable =
        [| for i in 1 .. 9 -> { Symbol = (string i); Position = i } |]


    let outOfRangePositionValid turn =
        match turn.Position with
        | pos when pos >= 0 && pos <= 8 -> Success turn
        | _ -> Failure "The position is out of range!"

    let isDecimalMatch number = Regex.IsMatch(number, "\\d")

    let notEmptyPiece turn =
        match turn.Table with
        | table when isDecimalMatch (table.[turn.Position].Symbol) -> Success turn
        | _ -> Failure "Then Cell is not empty!"


    let changeItemValue position value (arr: Cell []) =
        arr.[position] <- { Position = position; Symbol = value }
        arr

    let AddSymbolToCell symbol turn =
        turn.Table
        |> changeItemValue turn.Position symbol
        |> (fun table -> Success { turn with Table = table })

    let getRegex (arr: int []) =
        "^(?=.*"
        + (string arr.[0])
        + ")(?=.*"
        + (string arr.[1])
        + ")(?=.*"
        + (string arr.[2])
        + ").*$"

    let hasTrue x = x

    let isWin positions =
        [| [| 0; 1; 2 |]
           [| 3; 4; 5 |]
           [| 6; 7; 8 |]
           [| 0; 3; 7 |]
           [| 1; 4; 7 |]
           [| 2; 5; 8 |]
           [| 0; 4; 8 |]
           [| 2; 1; 6 |] |]
        |> Seq.map (fun arr -> Regex.IsMatch(positions, (getRegex arr)))
        |> Seq.exists hasTrue

    let won symbol turn =
        turn.Table
        |> Seq.where (fun table -> table.Symbol = symbol)
        |> Seq.map (fun table -> string table.Position)
        |> Seq.reduce (+)
        |> isWin
        |> function
        | true -> Won
        | _ -> Success turn

    let getNumber input =
        match input with
        | input when isDecimalMatch input -> Success(int input)
        | _ -> Failure "The input value is not a number!"

    let getPosition () =
        printf "Set Cell position: "
        Console.ReadLine() |> getNumber

    let runTurn symbol turn =
        getPosition ()
        >>= (fun pos -> Success { turn with Position = (pos - 1) })
        >>= outOfRangePositionValid
        >>= notEmptyPiece
        >>= AddSymbolToCell symbol
        >>= won symbol

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
