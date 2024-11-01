namespace NetwaysSql.Model
{
    public class FullProductDto
    {
         public Guid Id { get; set; }

         public string? Name { get; set; }

         public string? Description { get; set; }

         public decimal Price { get; set; }

         public CategoryDto? Category { get; set; }

         public ICollection<TagDto>? Tags { get; set; }

    }

     public class TagDto
     {
        public Guid Id { get; set; }

        public string? Name { get; set; }
     }
}
