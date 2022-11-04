
namespace Telephony.Modles
{
    using System;
    using System.Linq;

    using Interfaces;

    public class Stationaryphone : IPhone
    {
        public string Call(string phoneNumber)
        {
            if (!ValidateNumber(phoneNumber))
            {
                throw new ArgumentException("Invalid number!");
            }
            return $"Dialing... {phoneNumber}";
        }

        private bool ValidateNumber(string phoneNumber) 
        {
            if (phoneNumber.Length != 7 || !phoneNumber.All(ch => char.IsDigit(ch)))
            {
                return false;
            }
            else return true;
        }
    }
}
