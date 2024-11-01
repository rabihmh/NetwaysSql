namespace NetwaysSql.Model
{
    public class Product
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required decimal Price { get; set; }

        public required Guid CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<ProductTag>? ProductTags { get; set; }

    }
}
