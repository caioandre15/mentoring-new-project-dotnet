using Azure;
using Microsoft.Extensions.Hosting;

namespace ProjectCatalog.Models
{
    public class ProductAttribute
    {
        public Guid ProductId { get; set; }
        public Guid AttributeId { get; set; }
    }
}
