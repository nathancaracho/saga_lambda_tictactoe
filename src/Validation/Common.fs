namespace TicTacToe.Validation

open TicTacToe.Common

[<AutoOpen>]
module Common =
    let isDecimalMatch number = regexIsMatch number "\\d" 
