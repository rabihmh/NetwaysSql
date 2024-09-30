namespace NetwaysSql.Model
{
    using FluentValidation;

    public record UpdateCategoryDto
    {
        public Guid Id { get; init; }

        public required string Name { get; init; }

        public required string Description { get; init; }

    }
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.Description).MaximumLength(100).WithMessage("Description must not exceed 100 characters");
        }
    }
}
