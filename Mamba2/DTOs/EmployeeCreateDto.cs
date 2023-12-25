using FluentValidation;
using FluentValidation.AspNetCore;

namespace Mamba2.DTOs
{
    public class EmployeeCreateDto
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Desc {  get; set; }
        public string MediaLink {  get; set; }
        public List<int> PositionIds { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
    public class EmployeeCreateDtoValidator:AbstractValidator<EmployeeCreateDto>
    {
        public EmployeeCreateDtoValidator()
        {
            RuleFor(x => x.FullName).NotNull().WithMessage("Null olmaz")
            .MaximumLength(50).WithMessage("Max 50 ola biler!")
               .MinimumLength(3).WithMessage("Min 3 ola biler!");
            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(50).WithMessage("Max 50 ola biler!")
               .MinimumLength(3).WithMessage("Min 3 ola biler!");
            RuleFor(x => x.Desc)
               .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!")
              .MaximumLength(150).WithMessage("Max 150 ola biler!")
              .MinimumLength(3).WithMessage("Min 3 ola biler!");
            RuleFor(x => x.MediaLink)
               .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!")
              .MaximumLength(250).WithMessage("Max 250 ola biler!")
              .MinimumLength(3).WithMessage("Min 3 ola biler!");


        }
    }
}
