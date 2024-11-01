namespace NetwaysSql.Model
{
    public class Tag
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }

    }
}
