using FluentValidation;
using static NorthwindTradeSuite.Common.GlobalConstants.Entities.CategoryConstants;

namespace NorthwindTradeSuite.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CreateCategoryRequestDTO.Name)
                .NotEmpty()
                .WithMessage(REQUIRED_CATEGORY_NAME_ERROR_MESSAGE)
                .Length(4, 50)
                .WithMessage(CATEGORY_NAME_LENGTH_ERROR_MESSAGE);

            RuleFor(x => x.CreateCategoryRequestDTO.Description)
                .NotEmpty()
                .WithMessage(REQUIRED_CATEGORY_DESCRIPTION_ERROR_MESSAGE)
                .Length(10, 100)
                .WithMessage(CATEGORY_DESCRIPTION_LENGTH_ERROR_MESSAGE);

            RuleFor(x => x.CreateCategoryRequestDTO.Picture)
                .NotEmpty()
                .WithMessage(REQUIRED_CATEGORY_PICTURE_ERROR_MESSAGE);
        }
    }
}
