using FluentValidation;
using Application.Dtos;

namespace Application.Validators.Cat
{
    public class CatValidator : AbstractValidator<CatDto>
    {
        public CatValidator()
        {
            RuleFor(cat => cat.Name)
                .NotEmpty().WithMessage("Cat Name cannot be empty.")
                .NotNull().WithMessage("Cat Name cannot be null.");

            RuleFor(cat => cat.LikesToPlay)
                .NotNull().WithMessage("LikesToPlay must be specified.");
        }
    }
}
