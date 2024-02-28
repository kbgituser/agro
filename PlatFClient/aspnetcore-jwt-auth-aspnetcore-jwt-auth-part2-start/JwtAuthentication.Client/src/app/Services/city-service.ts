import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { CityDto } from '../Models/DTOs/city-dto';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  
  constructor(private http: HttpClient) { }
  citiesUrl="https://localhost:7272/api/cities"
  
  public getCities()
  {
    return this.http.get<CityDto[]>(this.citiesUrl);
  }
  
  public getCityById(id:string){
    return this.http.get<CityDto>(this.citiesUrl+"/"+id);
  }

  public updateCity(cityDto: CityDto)
  {
    return this.http.put<string>(this.citiesUrl, cityDto);
  }

  public deleteCityById(id: number)
  {
    return this.http.delete(this.citiesUrl+"/"+id);
  }
  public createCity(cityDto: CityDto){
    return this.http.post(this.citiesUrl,cityDto);
  }
  
}
