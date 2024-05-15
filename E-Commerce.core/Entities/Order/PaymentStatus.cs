using System.Text.Json.Serialization;

namespace E_Commerce.core.Entities.Order
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Pending, Failed, Received
    }
}
