import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CityDto } from '../../../Models/DTOs/city-dto';
import { CityService } from '../../../Services/city-service';

@Component({
  selector: 'app-cities',
  templateUrl: './city-index.component.html',
  styleUrls: ['./city-index.component.css'],
  providers: [CityService]
})
export class CityIndexComponent implements OnInit {

  constructor(private cityService: CityService, private router: Router) { 
    this.getCities();
  }

  cities: CityDto[];

  ngOnInit(): void {
   this.getCities(); 
  }

  getCities()
  {
    this.cityService.getCities().subscribe(
      (response: CityDto[]) =>
      {
        this.cities = response;
      }
        
      );
  }
  public deleteById(id: number)
  {
    this.cityService.deleteCityById(id).subscribe(response=>
      {
        this.getCities();
      });
  }

  public test ()
  {
    alert("test");
  }
  
}
