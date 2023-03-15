using System.Text.Json.Serialization;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;


namespace miniprojeto_samsys.Infrastructure
{
    public class MessagingHelper<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public T Obj { get; set; }

        [JsonConverter(typeof(SmartEnumValueConverter<ErrorType, string>))]
        public ErrorType? ErrorType { get; set; } = null;
    }

    public class MessagingHelper
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";

        [JsonConverter(typeof(SmartEnumValueConverter<ErrorType, string>))]
        public ErrorType? ErrorType { get; set; } = null;
    }

    public class ErrorType : SmartEnum<ErrorType, string>
    {
        public static readonly ErrorType DataHasChanged = new ErrorType("DataHasChanged");
        protected ErrorType(string name) : base(name, name) { }
    }
}
