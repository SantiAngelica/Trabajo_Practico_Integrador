namespace Domain.Entities;

public class Field
{
    public string Id { get; set; } = null!;
    public int FieldType { get; set; }
    public string PropertyId { get; set; } = null!;
    public Property Property { get; set; } = null!;

    public Field(int fieldType)
    {
        Id = Guid.NewGuid().ToString();
        FieldType = fieldType;
    }
}
