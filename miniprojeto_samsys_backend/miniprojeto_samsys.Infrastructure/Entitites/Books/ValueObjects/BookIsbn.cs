using System;
using miniprojeto_samsys.Infrastructure.Shared;
using Newtonsoft.Json;

namespace miniprojeto_samsys.Infrastructure.Entities.Books;

public class BookIsbn : EntityId{

    [JsonConstructor]
    public BookIsbn (String value) : base(value){}

    public override string AsString()
    {
        string obj = base.ObjValue.ToString();
        return obj;
    }

    protected override object createFromString(string text)
    {
        return text;
    }
}