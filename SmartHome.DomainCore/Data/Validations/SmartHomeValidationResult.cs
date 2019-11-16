using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace SmartHome.DomainCore.Data.Validations
{
    public class SmartHomeValidationResult
    {
        private SmartHomeValidationResult(IReadOnlyList<SmartHomeValidation> errors)
        {
            Errors = errors;
        }

        public bool Succeeded => Errors.Count == 0;
        public IReadOnlyList<SmartHomeValidation> Errors { get; }

        public static SmartHomeValidationResult Success { get; } = new SmartHomeValidationResult(ArraySegment<SmartHomeValidation>.Empty);

        public static SmartHomeValidationResult Failed(IReadOnlyList<SmartHomeValidation> errors)
        {
            return new SmartHomeValidationResult(errors.ToList());
        }
        
        public static SmartHomeValidationResult Failed(params SmartHomeValidation[] errors)
        {
            return new SmartHomeValidationResult(errors);
        }

        public static SmartHomeValidationResult FromIdentityResult(IdentityResult identityResult)
        {
            var errors = identityResult.Errors.Select(x => new SmartHomeValidation(x.Code, x.Description))
                .ToList();
            return new SmartHomeValidationResult(errors);
        }

        public SmartHomeValidationResult Merge(SmartHomeValidationResult result)
        {
            return new SmartHomeValidationResult(Errors.Concat(result.Errors).ToList());
        }
    }
}