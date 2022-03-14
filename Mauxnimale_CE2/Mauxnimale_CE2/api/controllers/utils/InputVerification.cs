using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Mauxnimale_CE2.api.controllers.utils
{
    internal class InputVerification
    {
        private static string _NUMBERS = "0123456789";
        private static string _SPECIALCHARS = "²&~#\"\'{([-|`_\\^@)°]=+}¨$¤£µ*%!§/:;.?,<>";

        /// <summary>
        /// Verify if the given text contains a number or not.
        /// </summary>
        /// <param name="text">The text to verify</param>
        /// <returns>true if it does not contain a number and false if it does.</returns>
        public static bool noNumber(string text)
        {
            foreach (char number in _NUMBERS)
            {
                if (text.Contains(number.ToString()))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Verify if the given text contains a special character or not.
        /// </summary>
        /// <param name="text">The text to verify</param>
        /// <returns>true if it does not contain a special character and false if it does.</returns>
        public static bool noSpecialCharacters(string text)
        {
            foreach (char specialChar in _SPECIALCHARS)
            {
                if (text.Contains(specialChar.ToString()))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Determine wether the given text is an email or not.
        /// </summary>
        /// <param name="text">The text to verify</param>
        /// <returns>true if the given text matches the email regex pattern, false otherwise.</returns>
        public static bool isEmail(string text)
        {
            try
            {
                new MailAddress(text);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Determine wether the given text is a phone number or not.
        /// </summary>
        /// <param name="text">The text to verify</param>
        /// <returns>true if the given text matches the phone number regex pattern, false otherwise.</returns>
        public static bool isPhoneNumber(string text)
        {
            return Regex.IsMatch(text, @"^(?:(?:\+|00)33[\s.-]{0,3}(?:\(0\)[\s.-]{0,3})?|0)[1-9](?:(?:[\s.-]?\d{2}){4}|\d{2}(?:[\s.-]?\d{3}){2})$");
        }

        /// <summary>
        /// Determine wether date1 is earlier than date2.
        /// </summary>
        /// <param name="date1">The date1 supposed to be earlier.</param>
        /// <param name="date2">The date2 supposed to be later.</param>
        /// <returns>true if date1 is earlier thant date2, false otherwise</returns>
        public static bool isDateEarlier(DateTime date1, DateTime date2)
        {
            return date1.CompareTo(date2) < 0;
        }
    }
}
