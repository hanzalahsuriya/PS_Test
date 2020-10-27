import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CountriesListComponent } from './countries-list/countries-list.component';
import { CountryDetailsComponent } from './country-details/country-details.component';

const routes: Routes = [
  {path:'country-list', component:CountriesListComponent},
  {path:'country-details/:code', component:CountryDetailsComponent},
  { path: '',   redirectTo: '/country-list', pathMatch: 'full' }, 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
