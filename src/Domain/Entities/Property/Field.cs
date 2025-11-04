namespace Domain.Entities;

public class Field
{
    public int Id { get; set; }
    public int FieldType { get; set; }
    public int PropertyId { get; set; }
    public Property Property { get; set; } = null!;

    public Field(int fieldType)
    {
        FieldType = fieldType;
    }
}
