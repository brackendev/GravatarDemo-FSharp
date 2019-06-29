module Tests

open Xunit

open Gravatar

[<Fact>]
let ``Gravatar.md5Hash test`` () =
    let test = Gravatar.md5Hash "email@example.com"
    Assert.Equal("5658ffccee7f0ebfda2b226238b1eb6e", test)

[<Fact>]
let ``Gravatar.imageURL test`` () =
    let test = Gravatar.imageURL "5658ffccee7f0ebfda2b226238b1eb6e"
    Assert.Equal("http://www.gravatar.com/avatar/5658ffccee7f0ebfda2b226238b1eb6e?s=2048&r=x", test)

[<Fact>]
let ``Gravatar.profileURL test`` () =
    let test = Gravatar.profileURL "email@example.com"
    Assert.Equal("http://www.gravatar.com/5658ffccee7f0ebfda2b226238b1eb6e.json", test) 
