using System;

namespace Paymentsense.Coding.Challenge.Api.RestCountries.Exceptions
{
    public class RestCountriesApiException : Exception
    {
        public RestCountriesApiException(string message) : base(message)
        {
        }
    }
}
