using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.Data
{
    public class ServiceResult<TType>
    {
        public ServiceResult(TType value, SmartHomeValidationResult validationResult)
        {
            Value = value;
            ValidationResult = validationResult;
        }

        public TType Value { get; }
        public SmartHomeValidationResult ValidationResult { get; }

        public bool Succeeded => ValidationResult.Succeeded;
    }
}