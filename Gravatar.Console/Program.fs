open Gravatar.Gravatar

[<EntryPoint>]
let main argv =
    match argv.Length with
    | 2 ->
        let x =
            match argv.[0] with
            | "image" -> getImage argv.[1]
            | "profile" -> getProfile argv.[1]
            | _ -> "Usage: <program> [image|profile] [email address]"
        printf "%s" x
        0
    | _ ->
        printf "Usage: <program> [image|profile] [email address]"
        0
