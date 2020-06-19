namespace TicTacToe.Validation

open TicTacToe.Validation
open TicTacToe.Common

[<AutoOpen>]
module InputValidation =
    let isNotNumberValid input =
        match input with
        | input when isDecimalMatch input -> Success(int input)
        | _ -> Failure "The input value is not a number!"
