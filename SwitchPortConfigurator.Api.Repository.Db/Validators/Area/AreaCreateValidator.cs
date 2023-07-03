using FluentValidation;
using SwitchPortConfigurator.Api.Repository.Entities;

namespace SwitchPortConfigurator.Api.Repository.Db.Validators.Area
{
    public class AreaCreateValidator : AbstractValidator<AreaEntity>
    {
        public AreaCreateValidator() 
        {
            RuleFor(a => a.Id).GreaterThanOrEqualTo(0);

        }
    }
}
