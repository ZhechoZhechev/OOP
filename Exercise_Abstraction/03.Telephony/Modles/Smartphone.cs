namespace Telephony.Modles
{
    using System;
    using System.Linq;

    using Interfaces;

    public class Smartphone : ISmartphone
    {

        public string Browse(string url)
        {
            if (!ValidateURLs(url)) 
            {
                throw new ArgumentException("Invalid URL!");
            }
            return $"Browsing: {url}!";
        }

        public string Call(string phoneNumber)
        {
            if (!ValidateNumber(phoneNumber))
            {
                throw new ArgumentException("Invalid number!");
            }
            return $"Calling... {phoneNumber}";
        }

        private bool ValidateNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 10 || !phoneNumber.All(ch => char.IsDigit(ch)))
            {
                return false;
            }
            else return true;
        }
        private bool ValidateURLs(string url) 
        {
            if (url.Any(ch => char.IsDigit(ch)))
            {
                return false;
            }
            else return true;
        }
    }
}
