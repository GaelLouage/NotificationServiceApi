using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructuur.Helpers
{
    public class EmailValidationHelper
    {
        public static (bool IsValid, string Message) IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, "Email cannot be empty.");
            email = email.Trim();
            var pattern = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
            var match = Regex.Match(email, pattern);

            return match.Success ? (true, "Email is valid.") : (false, "Email is invalid.");
        }
    }
}
