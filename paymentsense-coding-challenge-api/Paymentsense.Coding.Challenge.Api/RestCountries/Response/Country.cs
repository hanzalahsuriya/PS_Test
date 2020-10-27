using System.Collections.Generic;

namespace Paymentsense.Coding.Challenge.Api.RestCountries.Response
{
    public class Countries : List<Country>
    {

    }

    public class Country
    {
        public string Name { get; set; }
        public string[] TopLevelDomain { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public long Population { get; set; }
        public double[] Latlng { get; set; }

        public string Demonym { get; set; }
        public double? Area { get; set; }
        public double? Gini { get; set; }
        public string NativeName { get; set; }
        public string NumericCode { get; set; }
        public string Flag { get; set; }
        public string[] CallingCodes { get; set; }
        public string[] AltSpellings { get; set; }
        public string[] Timezones { get; set; }
        public string[] Borders { get; set; }
        public Currency[] Currencies { get; set; }
        public Languages[] Languages { get; set; }
        public RegionalBlocs[] RegionalBlocs { get; set; }
        public string Cioc { get; set; }
    }

    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class Languages
    {
        public string Iso639_1 { get; set; }
        public string Iso639_2 { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
    }


    public class RegionalBlocs
    {
        public string Acronym { get; set; }
        public string Name { get; set; }
        public string[] OtherAcronyms { get; set; }
        public string[] OtherNames { get; set; }
    }
}
