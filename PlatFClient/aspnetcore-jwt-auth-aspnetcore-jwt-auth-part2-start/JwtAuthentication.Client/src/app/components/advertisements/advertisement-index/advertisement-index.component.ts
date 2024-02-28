import { Component, OnInit } from '@angular/core';
import { AdvertisementDto } from 'src/app/Models/DTOs/Advertisement/advertisement-dto';
import { AdvertisementService } from 'src/app/Services/advertisement.service';

@Component({
  selector: 'app-advertisement-index',
  templateUrl: './advertisement-index.component.html',
  styleUrls: ['./advertisement-index.component.css']
})
export class AdvertisementIndexComponent implements OnInit {

  constructor(private advertisementService: AdvertisementService) { 
    this.getAdvertisements();
  }

  ngOnInit(): void {

  }

  advertisementDtos : AdvertisementDto[] = [];

  getAdvertisements()
  {
    this.advertisementService.getAll().subscribe(
      (response: AdvertisementDto[]) =>
      {
        this.advertisementDtos = response;
      }        
      );
  }
  public deleteById(id: number)
  {
    this.advertisementService.deleteById(id).subscribe(response=>
      {
        this.getAdvertisements();
      });
  }
}