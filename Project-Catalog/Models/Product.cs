﻿namespace ProjectCatalog.Models;

public class Product : Entity
{
    public string Sku { get; set; }
    public string Description { get; set; }
    public string Producer { get; set; }

    public List<Attribute> Attributes { get; } = new();
}
