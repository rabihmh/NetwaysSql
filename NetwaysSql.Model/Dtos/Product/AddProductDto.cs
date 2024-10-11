using FluentValidation;

namespace NetwaysSql.Model
{
    public record AddProductDto
    {
        public required string Name { get; init; }

        public string? Description { get; init; }

        public decimal Price { get; init; }

        public Guid CategoryId { get; init; }
    }

    public class AddProductValidator : AbstractValidator<AddProductDto>
    {
        public AddProductValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required");

            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.Description).MaximumLength(100).WithMessage("Description must not exceed 100 characters");

        }

    }
}
