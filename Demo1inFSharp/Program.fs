let f x = x * x

[<EntryPoint>]
let main argv = 
    let g y = y.ToString()

    let gf = g << f
    printfn "g nach f von 5 = %s" (gf 5)

    printfn "f nach f von 2 = %d" ((f<<f) 2)
    0
