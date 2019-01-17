using SEWKTand.Features.Shared.SSNRValidator.Interfaces;
using System;
using System.Globalization;

namespace SEWKTand.Features.Shared.Helpers
{
    public class SocialSecurityNumberVerification : ISocialSecurityNumberVerification
    {
        public bool VerifyIfSocialSecurityNumberIsValid(string ssnumber)
        {
            if (ssnumber.Length > 12 || ssnumber.Length < 10) return false; // ssnr must be between 10-12 digits long
            if (ssnumber.Length == 12) { ssnumber = ssnumber.Substring(2); } //If 12 digits - shorten with the two first digits.

            if (CheckIfDateTimeIsValid(ssnumber))
            {
                int month = int.Parse(ssnumber.Substring(2, 2)); // Start at third digit and end by fourth to get month.
                int day = int.Parse(ssnumber.Substring(4, 2)); // Start at fifth digit and end by sixth to get day.

                int totalSum = 0;
                int sum = 0;
                for (int i = 0; i < ssnumber.Length - 1; i++)
                {
                    sum = int.Parse(ssnumber.Substring(i, 1));
                    if ((i % 2) == 0) // Checks if 'i' is even. Multiply every number on a even position by two otherwise leave as is.
                    {
                        sum = int.Parse(ssnumber.Substring(i, 1)) * 2;
                        if (sum.ToString().Length > 1) // Check if the sum of the multiplication is larger than one digit long
                        {
                            sum = int.Parse(sum.ToString().Substring(0, 1)) + int.Parse(sum.ToString().Substring(1, 1)); // Split the digits and add them together
                        }
                    }
                    totalSum += sum;
                }

                double roundedUpToNearestTen = (Math.Ceiling(totalSum / 10.0d) * 10); // Round up totalSum to the nearest 10

                double tempControlDigit = roundedUpToNearestTen - totalSum;
                double controlDigit = (double.Parse(ssnumber)) % 10; // Takes the last digit from the social security number which is the controldigit

                if (tempControlDigit.Equals(controlDigit)) return true;
            }
            return false;
        }

        private bool CheckIfDateTimeIsValid(string ssnumber)
        {
            //TryParseExact - takes the first six digits and and tries to convert them to DateTime using the specific format, culture-specific format information and style.
             return DateTime.TryParseExact(ssnumber.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tempDate);
        }
    }
}
