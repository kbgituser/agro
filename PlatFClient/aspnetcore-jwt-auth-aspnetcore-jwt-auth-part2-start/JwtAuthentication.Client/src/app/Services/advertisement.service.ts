import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AdvertisementDto } from '../Models/DTOs/Advertisement/advertisement-dto';

@Injectable({
  providedIn: 'root'
})
export class AdvertisementService {

  constructor(private http: HttpClient) { }
  advertisementUrl = "https://localhost:7272/api/advertisements"
  public getAll()
  {
    return this.http.get<AdvertisementDto[]>(this.advertisementUrl);
  }
  
  public getById(id:string){
    return this.http.get<AdvertisementDto>(this.advertisementUrl+"/"+id);
  }

  public update(advertisementDto: AdvertisementDto)
  {
    return this.http.put<string>(this.advertisementUrl, advertisementDto);
  }

  public deleteById(id: number)
  {
    return this.http.delete(this.advertisementUrl+"/"+id);
  }
  public create(advertisementDto: AdvertisementDto){
    return this.http.post(this.advertisementUrl,advertisementDto);
  }
}
