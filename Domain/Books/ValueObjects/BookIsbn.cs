using System;
using miniprojeto_samsys.Domain.Shared;
using Newtonsoft.Json;

namespace miniprojeto_samsys.Domain.Books;

public class BookIsbn : EntityId{

    [JsonConstructor]
    public BookIsbn (int code) : base(code){}

    public override string AsString()
    {
        int obj = (int) base.ObjValue;
        return obj.ToString();
    }

    protected override object createFromString(string text)
    {
        return Int32.Parse(text);
    }
}