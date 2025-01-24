using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("please enter name");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("must be minimum 1");
            RuleFor(X => X.Price).GreaterThan(0).WithMessage("must be greater 0 ");
            RuleFor(x => x.Description).MinimumLength(10).MaximumLength(155).WithMessage("must be decsription characters count is 10 - 155");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Type is incorrect ");
            RuleFor(x => x.Photo).Must(IsCorrrectFormat).When(x => x.Photo is not null).WithMessage("Incorrect format");
        }

        private bool IsCorrrectFormat(string photo)
        {

            try
            {
                _ = Convert.FromBase64String(photo);
                var data = photo.Substring(0, 5);
                switch (data.ToUpper())
                {
                    case "IVBOR":
                    case "/9J/4":
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
