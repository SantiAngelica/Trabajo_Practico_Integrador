namespace Application.Models;

public record RequestUserDto(
    string Name,
    string Email,
    string Password,
    int Age,
    string Zone,
    List<int> FieldsType,
    List<string> Positions
);

public record UpdateUserDto(
    string Name,
    string Email,
    int Age,
    string Zone,
    List<int> FieldsType,
    List<string> Positions
);

public record LoginRequestUserDto(string Email, string Password);
