namespace ProjectCatalog.Models
{
    public class Attribute : Entity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
