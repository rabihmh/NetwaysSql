using FluentValidation;

namespace NetwaysSql.Model
{
    public record UpdateProductDto
    {
        public required Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public Guid? CategoryId { get; set; }
    }

    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.Description).MaximumLength(100).WithMessage("Description must not exceed 100 characters");
           
        }

    }
}
