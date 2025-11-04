namespace Application.Models;

public record RequestGameDto(
    int Property_id,
    DateOnly Date,
    int Schedule_id,
    int Field_id,
    int Missing_players
);

public record RequestGameOnylDto(
    DateOnly Date,
    string PropertyName,
    string PropertyZone,
    string PropertyAdress,
    int Schedule,
    int FieldType,
    int Missing_players
);
