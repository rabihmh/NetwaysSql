
namespace NetwaysSql.Model
{
    public record ProductWithCategoryDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

       public CategoryDto? Category { get; set; }
    }
}
