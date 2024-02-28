import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryDto } from '../Models/DTOs/category-dto';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }
  url="https://localhost:7272/api/Categories"
  
  public getCategories()
  {
    return this.http.get<CategoryDto[]>(this.url);
  }
  
  public getCategoryById(id:string)
  : Observable<CategoryDto>
  {
    return this.http.get<CategoryDto>(this.url+"/"+id);
  }

  public updateCategory(categoryDto: CategoryDto): Observable<string>
  {
    return this.http.put<string>(this.url, categoryDto);
  }
}
