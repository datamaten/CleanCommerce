using Domain.Entities;

namespace Application.UseCases.Products.Queries.GetProducts;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
