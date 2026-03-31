using System.Text.RegularExpressions;

namespace FamilyApplication.utils
{
    public static class InfosValidation
    {
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (Regex.IsMatch(email, pattern))
                return true;
            return false;
        }

        public static bool IsValidPhone(string phone)
        {
            string pattern = @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$";

            if (Regex.IsMatch(phone, pattern))
                return true;
            return false;
        }
    }
}
