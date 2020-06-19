namespace TicTacToe.Common

open System.Text.RegularExpressions


[<AutoOpen>]
module Common =
    
    type GameState<'TSuccess> =
    | Success of 'TSuccess
    | Failure of string
    | Won of string

    let bind result func =
        match result with
        | Success sResult -> func sResult
        | Failure fResult -> Failure fResult
        | Won wResult -> Won wResult

    let (>>=) result func = bind result func

    let isTrue x = x

    let regexIsMatch str pattern = Regex.IsMatch (str, pattern)