export interface CountryShortResponse {
    code: string,
    name: string,
    flag: string
}

export interface Country {
    name: string,
    alpha2Code: string,
    alpha3Code: string,
    capital: string,
    topLevelDomain: string[],
    region: string,
    subregion: string,
    population: number,
    latlng: number[],
    demonym: string,
    area: number,
    gini: number,
    nativeName: string,
    numericCode: string,
    flag: string,
    callingCodes: string[],
    altSpellings: string[],
    timezones: string[],
    borders: string[],
    currencies: Currency[],
    languages: Language[],
    cioc: string,
}

interface Currency {
    code: string
    name: string
    symbol: string
}

interface Language {
    iso639_1: string
    iso639_2: string
    name : string
    nativeName : string
}