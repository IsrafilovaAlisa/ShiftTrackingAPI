using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ShiftTrackingAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Position
    {
        [Description("Менеджер")]
        Manager = 0,
        [Description("Инженер")]
        Engineer = 1,
        [Description("Тестер свечек")]
        Tester = 2
    }
}
