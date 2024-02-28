import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, observable } from 'rxjs';
import { CityDto } from 'src/app/Models/DTOs/city-dto';
import { CityService } from 'src/app/Services/city-service';

@Component({
  selector: 'app-update',
  templateUrl: './city-update.component.html',
  styleUrls: ['./city-update.component.css']
})
export class CityUpdateComponent implements OnInit {
  
  cityDto:CityDto = new CityDto();
  cityId: string;
  create : boolean = false;
  edit: boolean = false;
  delete: boolean = false;

  constructor(private cityService: CityService, private router: Router, private route: ActivatedRoute) { 
    this.cityId = this.route.snapshot.paramMap.get('id');
    const url = this.router.url;

    this.create = url.includes('create');
    this.edit = url.includes('edit');
    this.delete = url.includes('delete');

    if (this.cityId){      
      cityService.getCityById(this.cityId).subscribe(response=> 
        this.cityDto = response
        );
    }
  }

  ngOnInit(): void {
   
  }

  public updateCity()
  {
      this.cityService.updateCity(this.cityDto).subscribe(Response => {
          this.router.navigate(['/cities']);  // go to cities page
      });
  }

  public createCity(){
    this.cityService.createCity(this.cityDto).subscribe(response=>{
      this.router.navigate(['/cities']);
    });
  }

}
