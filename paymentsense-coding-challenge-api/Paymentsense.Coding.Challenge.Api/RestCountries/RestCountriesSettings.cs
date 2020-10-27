namespace Paymentsense.Coding.Challenge.Api.RestCountries
{
    public class RestCountriesSettings
    {
        public static string SettingsKey = "RestCountries";

        public string APIURL { get; set; }

        public int TimeToCacheInMinutes { get; set; }
    }
}
