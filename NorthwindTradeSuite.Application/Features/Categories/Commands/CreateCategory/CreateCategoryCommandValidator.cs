using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.CategoryConstants;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CreateCategoryRequestDTO.Name)
                .NotEmpty()
                .WithMessage(REQUIRED_CATEGORY_NAME_ERROR_MESSAGE);

            RuleFor(x => x.CreateCategoryRequestDTO.Description)
                .NotEmpty()
                .WithMessage(REQUIRED_CATEGORY_DESCRIPTION_ERROR_MESSAGE);

            RuleFor(x => x.CreateCategoryRequestDTO.Picture)
                .NotEmpty()
                .WithMessage(REQUIRED_CATEGORY_PICTURE_ERROR_MESSAGE);
        }
    }
}
