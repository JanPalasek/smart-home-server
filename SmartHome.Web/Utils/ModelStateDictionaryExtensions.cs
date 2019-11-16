using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.Web.Utils
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddValidationErrors(this ModelStateDictionary modelState,
            SmartHomeValidationResult smartHomeValidationResult)
        {
            if (smartHomeValidationResult.Succeeded)
            {
                return;
            }

            foreach (var validation in smartHomeValidationResult.Errors)
            {
                modelState.AddModelError(validation.Field, validation.Message);
            }
        }
    }
}