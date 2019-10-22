using System.ComponentModel.DataAnnotations;

namespace SmartHome.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Verifies whether the given string is email address.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
        /// </remarks>
        public static bool IsEmail(this string? address)
        {
            return address != null && new EmailAddressAttribute().IsValid(address);
        }
    }
}