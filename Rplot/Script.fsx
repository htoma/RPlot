#load "../packages/FsLab.0.3.20/FsLab.fsx"
#load "../packages/RProvider.1.1.20/RProvider.fsx"

#load "ggplot.fs"

open RProvider
open RProvider.ggplot2
open Deedle

open ggplot

open RProvider.datasets
let mtc = R.mtcars.GetValue<Frame<string, string>>()

let (++) (plot1:RDotNet.SymbolicExpression) (plot2:RDotNet.SymbolicExpression) = 
    R.``+``(plot1, plot2) 

R.ggplot(
    namedParams[
        "data", box mtc; 
        "mapping", box (
            R.aes__string(x="disp", y="drat"))])
++ R.geom__point()

G.ggplot(mtc, G.aes(x="disp", y="drat"))
++ R.geom__point()


let iris = R.iris.GetValue<Frame<string, string>>()

let sizeSettings () =
    R.theme(namedParams["axis.text", R.element__text(namedParams["size", 12])])
    ++ R.theme(namedParams["legend.text", R.element__text(namedParams["size", 12])])
    ++ R.theme(namedParams["axis.title", R.element__text(namedParams["size", 14])])
    ++ R.theme(namedParams["plot.title", R.element__text(namedParams["size", 18])])

// Create a plot
G.ggplot(iris, G.aes(x="Sepal.Length", y="Sepal.Width",colour="Petal.Length"))
++ R.geom__point(namedParams["size", 4])
++ R.theme__bw()
++ R.scale__color__gradient(
    namedParams["low", "blue"; "high", "green"])
++ R.ggtitle("Iris dataset")
++ R.xlab("Sepal length")
++ R.ylab("Sepal width")
++ sizeSettings()