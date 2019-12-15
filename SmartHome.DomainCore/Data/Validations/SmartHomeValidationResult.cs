using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace SmartHome.DomainCore.Data.Validations
{
    public class SmartHomeValidationResult
    {
        private SmartHomeValidationResult(bool succeeded, IReadOnlyList<SmartHomeValidation> errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }

        public bool Succeeded { get; }
        public IReadOnlyList<SmartHomeValidation> Errors { get; }

        public static SmartHomeValidationResult Success { get; } = new SmartHomeValidationResult(true, ArraySegment<SmartHomeValidation>.Empty);

        public static SmartHomeValidationResult Failed(IReadOnlyList<SmartHomeValidation> errors)
        {
            return new SmartHomeValidationResult(false, errors.ToList());
        }
        
        public static SmartHomeValidationResult Failed(params SmartHomeValidation[] errors)
        {
            return new SmartHomeValidationResult(false, errors);
        }

        public static SmartHomeValidationResult FromIdentityResult(IdentityResult identityResult)
        {
            var errors = identityResult.Errors.Select(x => new SmartHomeValidation(x.Code, x.Description))
                .ToList();
            return new SmartHomeValidationResult(errors.Count == 0, errors);
        }

        public static SmartHomeValidationResult FromSignInResult(SignInResult signInResult)
        {
            if (signInResult.Succeeded)
            {
                return Success;
            }
            
            // do not map properly error by error => it is information leak for the attacker
            return Failed(new SmartHomeValidation("Credentials", "Credentials are invalid."));
        }

        public SmartHomeValidationResult Merge(SmartHomeValidationResult result)
        {
            var mergedErrors = Errors.Concat(result.Errors).ToList();
            return new SmartHomeValidationResult(Succeeded && result.Succeeded, mergedErrors);
        }

        public override string ToString()
        {
            return string.Join(',', Errors.Select(x => x.ToString()));
        }
    }
}