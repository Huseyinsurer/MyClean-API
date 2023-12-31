using FluentValidation;
using Application.Dtos;

namespace Application.Validators.Dog
{
    public class DogValidator : AbstractValidator<DogDto>
    {
        public DogValidator()
        {
            RuleFor(dog => dog.Name)
                .NotEmpty().WithMessage("Dog Name cannot be empty.")
                .NotNull().WithMessage("Dog Name cannot be null.");

            RuleFor(dog => dog.Breed)
                .NotEmpty().WithMessage("Dog Breed cannot be empty.")
                .NotNull().WithMessage("Dog Breed cannot be null.");

            RuleFor(dog => dog.Weight)
                .NotNull().WithMessage("Weight must be specified.")
                .GreaterThan(0).WithMessage("Weight must be greater than 0.");
        }
    }
}
