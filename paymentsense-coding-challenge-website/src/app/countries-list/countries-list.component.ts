import { Component } from '@angular/core';
import { PaymentsenseCodingChallengeApiService } from '../services/paymentsense-coding-challenge-api.service';
import { CountryShortResponse } from '../services/Country.model'

@Component({
  selector: 'countries-list',
  templateUrl: './countries-list.component.html',
  styleUrls: ['./countries-list.component.scss']
})
export class CountriesListComponent {
  public countries: CountryShortResponse[] = [];
  config: any;

  constructor(private paymentsenseCodingChallengeApiService: PaymentsenseCodingChallengeApiService) {
    paymentsenseCodingChallengeApiService.getCountries().subscribe(countries => {
      this.countries = countries;

      this.config = {
        itemsPerPage: 30,
        currentPage: 1,
        totalItems: this.countries.length
      };
    })
  }

  pageChanged(event){
    this.config.currentPage = event;
  }

}
