namespace DDC.Libs

open System;

module ListenMitFSharp =

    /// Tail-recursive FoldR Funktion
    /// verwendet Continuation-passing um
    /// vom Stack auf den Heap zu kommen
    let rec private trFoldR (l : 'a Liste) 
                            (f : ('a*'s) -> 's) 
                            (s : 's) 
                            (cont : 's -> 's) 
                            =
        if Listen.IsEmpty l 
            then cont s 
            else trFoldR (Listen.Tail l) f s
                            (fun r -> f (Listen.Head l, r) |> cont)

    /// FoldR - C# freundlich - ruft trFoldR auf
    let FoldR (l : 'a Liste, f : Func<'a,'s,'s>, s : 's) =
        trFoldR l f.Invoke s id