namespace NetwaysSql.Model
{
    public record ProductDto
    {
        public required Guid Id { get; init; }

        public string? Name { get; init; }

        public string? Description { get; init; }

        public decimal Price { get; init; }

        public required Guid CategoryId { get; init; }

    }
}
