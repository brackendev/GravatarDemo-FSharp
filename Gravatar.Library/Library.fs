namespace Gravatar

open System
open System.Net
open FSharp.Data

module Gravatar =
    let md5Hash (input : string) : string =
        use hash = System.Security.Cryptography.MD5.Create()
        input
        |> System.Text.Encoding.ASCII.GetBytes
        |> hash.ComputeHash
        |> Seq.map (fun c -> c.ToString("x2"))
        |> Seq.reduce (+)

    let imageURL (emailHash : string) =
        "http://www.gravatar.com/avatar/" + emailHash + "?s=2048&r=x"

    let profileURL (email : string) =
        let emailHash = md5Hash email
        "http://www.gravatar.com/" + emailHash + ".json"

    let fetchUrl callback url =        
        let req = WebRequest.Create(Uri(url)) :?> HttpWebRequest
        req.UserAgent <- "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_5) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.1.1 Safari/605.1.15"
        use resp = req.GetResponse() 
        use stream = resp.GetResponseStream() 
        use reader = new IO.StreamReader(stream) 
        callback reader url

    let profileCallback (reader : IO.StreamReader) url = 
        reader.ReadToEnd()

    let downloadImage (email : string) =
        let emailHash = md5Hash email
        let response = Http.Request(imageURL emailHash)
        match response.StatusCode with
        | 200 -> 
            match response.Body with
            | FSharp.Data.Binary b ->
                if response.Headers.["Content-Type"] = "image/jpeg" then (emailHash + ".jpg", b)
                elif response.Headers.["Content-Type"] = "image/png" then (emailHash + ".png", b)
                else (emailHash, b)
            | _ -> (null, null)
        | _ -> (null, null)

    // Download the image for the email address
    let getImage (email : string) =
        let (fileName, bytes) = downloadImage email
        System.IO.File.WriteAllBytes(fileName, bytes)
        "Downloaded image to " + fileName

    // Retrieve the profile for the email address
    let getProfile (email : string) =
        fetchUrl profileCallback (profileURL email)    
