using System.Runtime.Serialization;

namespace Domain.Enum;

public enum ParticipationType
{
    Invitacion,
    Postulacion,
}

public enum States
{
    [EnumMember(Value = "pendiente")]
    Pendiente,

    [EnumMember(Value = "aceptada")]
    Aceptada,

    [EnumMember(Value = "rechazada")]
    Rechazada,
}
