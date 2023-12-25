using FluentValidation;

namespace Mamba2.DTOs
{
    public class PositionCreateDto
    {
        public string Name {  get; set; }

    }
    public class PositionCreateDtoValidator : AbstractValidator<PositionCreateDto>
    {
        public PositionCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Null olmaz")
            .MaximumLength(50).WithMessage("Max 50 ola biler!")
               .MinimumLength(3).WithMessage("Min 3 ola biler!");
      

        }
    }
}
