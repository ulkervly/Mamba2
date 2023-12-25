using FluentValidation;

namespace Mamba2.DTOs
{
    public class PositionGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class PositionGetDtoValidatior : AbstractValidator<PositionGetDto>
    {
        public PositionGetDtoValidatior()
        {
            RuleFor(x => x.Name)
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(50).WithMessage("Max 50 ola biler!")
               .MinimumLength(5).WithMessage("Min 3 ola biler!");
          
        }
    }
}
