﻿using FluentValidation;

namespace Mamba2.DTOs
{
    public class EmployeeUpdateDto
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Desc {  get; set; }
        public string MediaLink {  get; set; }
        public List<int> PositionIds { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
    public class EmployeeUpdateDtoValidator : AbstractValidator<EmployeeUpdateDto>
    {
        public EmployeeUpdateDtoValidator()
        {
            RuleFor(x => x.FullName).NotNull().WithMessage("Null olmaz")
            .MaximumLength(50).WithMessage("Max 50 ola biler!")
               .MinimumLength(3).WithMessage("Min 3 ola biler!");
            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(50).WithMessage("Max 50 ola biler!")
               .MinimumLength(3).WithMessage("Min 3 ola biler!");

        }
    }
}
