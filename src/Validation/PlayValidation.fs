namespace TicTacToe.Validation

open TicTacToe.Common
open TicTacToe.Validation
open TicTacToe.DomainType

[<AutoOpen>]
module PlayValidation =

    let outOfRangePositionValid turn =
        match turn.Position with
        | pos when pos >= 0 && pos <= 8 -> Success turn
        | _ -> Failure "The position is out of range!"

    let notEmptyPiece turn =
        match turn.Table with
        | table when isDecimalMatch (table.[turn.Position].Symbol) -> Success turn
        | _ -> Failure "Then Cell is not empty!"
