using System;
using miniprojeto_samsys.Infrastructure.Shared;
using Newtonsoft.Json;

namespace miniprojeto_samsys.Infrastructure.Entities.Authors;

public class AuthorId : EntityId
{

    [JsonConstructor]
    public AuthorId (Guid value): base(value){

    }

    public AuthorId (String value): base(value){

    }

    public override string AsString()
    {
            Guid obj = (Guid) base.ObjValue;
            return obj.ToString();    
    }

    protected override object createFromString(string text)
    {
        return new Guid(text);
    }

    public Guid AsGuid(){
        return (Guid) base.ObjValue;
    }
}