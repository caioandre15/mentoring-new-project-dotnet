namespace ProjectCatalog.Models;

public class Attribute : Entity
{
    public string Name { get; set; }
    public string Value { get; set; }

    public List<Product> Products { get; } = new();
}
