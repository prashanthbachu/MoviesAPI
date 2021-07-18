using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoviesAPI
{
    internal class DoubleConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("0.0"));
        }
    }
}