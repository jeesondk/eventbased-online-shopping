namespace Common.Domain.Product.Entities;

public class Product
{
    public int Id { get; set; } = 0;
    public long Sku { get; set; } = 0L;
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; } = 0L;
}