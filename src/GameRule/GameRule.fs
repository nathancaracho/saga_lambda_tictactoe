namespace TicTacToe.GameRule

open TicTacToe.Common
open TicTacToe.DomainType
open TicTacToe.Validation.PlayValidation
open TicTacToe.IO

[<AutoOpen>]
module GameRule =



    let changeItemValue position value (arr: Cell []) =
        arr.[position] <- { Position = position; Symbol = value }
        arr

    let AddSymbolToCell symbol turn =
        turn.Table
        |> changeItemValue turn.Position symbol
        |> (fun table -> Success { turn with Table = table })

    let getRowRegex (arr: int []) =
        arr
        |> Seq.fold (fun str next -> str + "(?=.*" + (string next) + ")") ""
        |> (fun regex -> "^" + regex + ".*$")

    let isWin positions =
        [| [| 0; 1; 2 |]
           [| 3; 4; 5 |]
           [| 6; 7; 8 |]
           [| 0; 3; 7 |]
           [| 1; 4; 7 |]
           [| 2; 5; 8 |]
           [| 0; 4; 8 |]
           [| 2; 1; 6 |] |]
        |> Seq.map (fun arr -> regexIsMatch positions (getRowRegex arr))
        |> Seq.exists isTrue

    let wasWon symbol turn =
        turn.Table
        |> Seq.where (fun table -> table.Symbol = symbol)
        |> Seq.map (fun table -> string table.Position)
        |> Seq.reduce (+)
        |> isWin
        |> function
        | true -> Won("The winner is " + symbol)
        | _ -> Success turn

    let runTurn symbol turn =
        getPosition ()
        >>= (fun pos -> Success { turn with Position = (pos - 1) })
        >>= outOfRangePositionValid
        >>= notEmptyPiece
        >>= AddSymbolToCell symbol
        >>= wasWon symbol
