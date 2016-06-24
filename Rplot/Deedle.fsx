#load "../packages/FsLab.0.3.20/FsLab.fsx"
#load "../packages/RProvider.1.1.20/RProvider.fsx"

open System
open Deedle
open FSharp.Charting

// Create from sequence of keys and sequence of values
let dates  = 
  [ DateTime(2013,1,1)
    DateTime(2013,1,4) 
    DateTime(2013,1,8) ]
let values = 
  [ 10.0; 20.0; 30.0 ]
let first = Series(dates, values)

// Create from a single list of observations
Series.ofObservations
  [ DateTime(2013,1,1) => 10.0
    DateTime(2013,1,4) => 20.0
    DateTime(2013,1,8) => 30.0 ]

// Shorter alternative to 'Series.ofObservations'
series [ 1 => 1.0; 2 => 2.0 ]

// Create series with implicit (ordinal) keys
Series.ofValues [ 10.0; 20.0; 30.0 ]

/// Generate date range from 'first' with 'count' days
let dateRange (first:System.DateTime) count = seq {for i in 0..(count-1) -> first.AddDays(float i)}

/// Generate 'count' number of random doubles
let rand count = 
    let rnd = System.Random()
    seq {for i in 0..(count-1) -> rnd.NextDouble()}

// A series with values for 10 days 
let second = Series(dateRange (DateTime(2013,1,1)) 10, rand 10)

let df1 = Frame(["first"; "second"], [first; second])

// The same as previously
let df2 = Frame.ofColumns ["first" => first; "second" => second]

// Transposed - here, rows are "first" and "second" & columns are dates
let df3 = Frame.ofRows ["first" => first; "second" => second]

// Create from individual observations (row * column * value)
let df4 = 
  [ ("Monday", "Tomas", 1.0); ("Tuesday", "Adam", 2.1)
    ("Tuesday", "Tomas", 4.0); ("Wednesday", "Tomas", -5.4) ]
  |> Frame.ofValues

let msftCsv = Frame.ReadCsv(__SOURCE_DIRECTORY__ + "../data/msft.csv")
let fbCsv = Frame.ReadCsv(__SOURCE_DIRECTORY__ + "/data/stocks/FB.csv")