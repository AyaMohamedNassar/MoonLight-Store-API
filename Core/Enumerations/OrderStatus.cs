using System.Runtime.Serialization;

namespace Core.Enumerations
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Recieved")]
        PaymentRecieved,
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
}
