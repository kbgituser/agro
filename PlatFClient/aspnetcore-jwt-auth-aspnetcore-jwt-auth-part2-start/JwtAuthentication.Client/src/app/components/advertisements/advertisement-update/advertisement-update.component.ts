import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdvertisementDto } from 'src/app/Models/DTOs/Advertisement/advertisement-dto';
import { AdvertisementService } from 'src/app/Services/advertisement.service';

@Component({
  selector: 'app-advertisement-update',
  templateUrl: './advertisement-update.component.html',
  styleUrls: ['./advertisement-update.component.css']
})
export class AdvertisementUpdateComponent implements OnInit {
  
  dto:AdvertisementDto = new AdvertisementDto();
  id: string;
  create : boolean ;
  edit: boolean = false;
  delete: boolean = false;

  constructor(private advertisementService: AdvertisementService, 
    private router: Router, 
    private route: ActivatedRoute) { 
      this.id = this.route.snapshot.paramMap.get('id');
      const url = this.router.url;
  
      this.create = url.includes('create');
      this.edit = url.includes('edit');
      this.delete = url.includes('delete');
  
      if (this.id){      
        advertisementService.getById(this.id).subscribe(response=> 
          this.dto = response
          );
      }
    }

  ngOnInit(): void {
  }

  
  public updateDto()
  {
      this.advertisementService.update(this.dto).subscribe(Response => {
          this.router.navigate(['/advertisements']);  // go to cities page
      });
  }

  public createDto(){
    this.advertisementService.create(this.dto).subscribe(response=>{
      this.router.navigate(['/advertisements']);
    });
  }

}
