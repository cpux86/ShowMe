using FluentValidation;
using Application.Features.Catalog.Commands.UpdateCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalog.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidation : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidation()
        {
            RuleFor(updateCategoryCommand =>
            updateCategoryCommand.Title).NotEmpty().MaximumLength(32);
        }
    }
}
