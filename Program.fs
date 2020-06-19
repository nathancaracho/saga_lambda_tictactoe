// Learn more about F# at http://fsharp.org

open System
open TicTacToe.Common
open TicTacToe.IO
open TicTacToe.GameRule
open TicTacToe.DomainType

[<EntryPoint>]
let main argv =
     let players = [|runTurn "X"; runTurn "O"|]

     let failureRun run turn turnIndex failure =
          printf "%s\n" failure
          run turn turnIndex

     let rec run turn turnIndex = 
         printTable turn.Table
         match players.[turnIndex % 2] turn with
         | Success result -> run result (turnIndex+1)
         | Failure result -> failureRun run turn turnIndex result
         | Won w -> printf "%s\n" w
         |>ignore

     run {Table = createTable; Position = 0} 0
     0 // return an integer exit code
