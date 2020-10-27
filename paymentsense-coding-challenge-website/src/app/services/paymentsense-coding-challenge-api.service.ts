import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { CountryShortResponse, Country } from "./Country.model";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class PaymentsenseCodingChallengeApiService {
  apiBaseUrl = "https://localhost:5001/";

  constructor(private httpClient: HttpClient) {}

  public getHealth(): Observable<string> {
    return this.httpClient.get(`${this.apiBaseUrl}health`, {
      responseType: "text",
    });
  }

  public getCountries(): Observable<CountryShortResponse[]> {
    return this.httpClient
      .get<CountryShortResponse[]>(`${this.apiBaseUrl}countries`)
      .pipe(catchError(this.handleError));
  }

  public getCountryDetails(code: string): Observable<Country> {
    return this.httpClient
      .get<Country>(`${this.apiBaseUrl}countries/${code}`)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error("An error occurred:", error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` + `body was: ${error.error}`
      );
    }
    return throwError("Something bad happened. Please try again later.");
  }
}
