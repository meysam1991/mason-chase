using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Shared.ValidatorExtensions
{
    public static class ValidatorExtensions
    {
        public static bool IsValidPhoneNumberAndCode(this string phoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(phoneNumber))
                    return false;
                const string phoneNumberRegularExpression = "^(0)[0-9]{2}-\\d{8}$";
                var isMatch = Regex.IsMatch(phoneNumber, phoneNumberRegularExpression);
                return isMatch;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(phoneNumber))
                    return false;
                const string phoneNumberRegularExpression = "^\\d{8}$";
                var isMatch = Regex.IsMatch(phoneNumber, phoneNumberRegularExpression);
                return isMatch;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidWebSiteUrl(this string webSiteUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(webSiteUrl))
                    return false;
                const string urlRegularExpression =
                    "((http(s)?:\\/\\/www\\.([a-zA-Z0-9]+(([a-zA-Z0-9]+)|(-))[a-zA-Z0-9.]+)(\\.)([a-zA-Z0-9-.]+))|(www\\.([a-zA-Z0-9]+(([a-zA-Z0-9]+)|(-))[a-zA-Z0-9]+)\\.[a-zA-Z0-9.-]+)|(http(s)?:\\/\\/([a-zA-Z0-9]+(([a-zA-Z0-9]+)|(-))[a-zA-Z0-9]+)\\.([a-zA-Z0-9-]+)))$";
                var isMatch = Regex.IsMatch(webSiteUrl, urlRegularExpression, RegexOptions.IgnoreCase);
                return isMatch;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidEmail(this string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return false;
                const string emailRegularExpression =
                    "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(\\.[a-zA-Z0-9]{1,61})$";
                var isMatch = Regex.IsMatch(email, emailRegularExpression, RegexOptions.IgnoreCase);
                return isMatch;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidCellPhone(this string cellphoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(cellphoneNumber))
                    return false;
                const string cellPhoneRegularExpression = "^09\\d{2}\\d{3}\\d{4}$";
                var isMatch = Regex.IsMatch(cellphoneNumber, cellPhoneRegularExpression);
                return isMatch;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPersonalNationalCode(this string nationalCode)
        {
            if (string.IsNullOrEmpty(nationalCode))
                return false;
            if (!Regex.IsMatch(nationalCode, @"^\d{10}$"))
                return false;

            var check = Convert.ToInt32(nationalCode.Substring(9, 1));
            var sum =
                Enumerable.Range(0, 9).Select(x => Convert.ToInt32(nationalCode.Substring(x, 1)) * (10 - x)).Sum() % 11;

            return sum < 2 && check == sum || sum >= 2 && check + sum == 11;
        }
        /// <summary>
        /// must  be 10?
        /// </summary>
        /// <param name="nationalCode"></param>
        /// <returns></returns>
        public static bool IsValidLegalNationalCode(this string nationalCode)
        {
            return !string.IsNullOrEmpty(nationalCode) && Regex.IsMatch(nationalCode, @"^\d{11}$");
        }

        public static bool IsValidPostalCode(this string postalCode)
        {
            if (!Regex.IsMatch(postalCode, "^[0-9]{10,10}$"))
                return false;
            if (postalCode.ToCharArray()[0] == '0')
                return false;
            return true;
        }

        public static bool IsValidBankCardNumber(this string cardNumber)
        {
            if (cardNumber.Any(c => !char.IsDigit(c)))
            {
                return false;
            }

            return cardNumber.Length == 16;
        }

        public static bool IsValidIbanNumber(this string iban)
        {
            if (iban.Length != 26)
                return false;

            var ibanPartCCCD = iban.Substring(0, 4);
            var ibanPartBBAN = iban.Substring(5, 21);
            var stringIbanNumber = $"{ibanPartBBAN}{ibanPartCCCD}";
            var result = stringIbanNumber.Replace("I", "18").Replace("R", "27");
            var decimalIbanNumber = decimal.Parse(result);
            var reminder = decimalIbanNumber % 97;
            return reminder == 1;
        }

        public static bool IsValidAccountNumber(this string accountNumber)
        {
            if (accountNumber.Any(c => !char.IsDigit(c)))
            {
                return false;
            }

            return accountNumber.Length <= 24;
        }
    }
}