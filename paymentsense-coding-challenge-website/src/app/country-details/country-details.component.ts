import { Component, Input } from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { PaymentsenseCodingChallengeApiService } from '../services/paymentsense-coding-challenge-api.service';
import { Country } from '../services/Country.model'

@Component({
  selector: 'country-details',
  templateUrl: './country-details.component.html',
  styleUrls: ['./country-details.component.scss']
})
export class CountryDetailsComponent {
  @Input() event:any
  public country: Country;

  constructor(private route: ActivatedRoute, private paymentsenseCodingChallengeApiService: PaymentsenseCodingChallengeApiService) {
    const code = this.route.snapshot.paramMap.get('code');
    
    paymentsenseCodingChallengeApiService.getCountryDetails(code).subscribe(country => {
      this.country = country;
    })
  }
}
