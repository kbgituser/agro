import { Component, OnInit } from '@angular/core';
import { CategoryDto } from 'src/app/Models/DTOs/category-dto';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-category-index',
  templateUrl: './category-index.component.html',
  styleUrls: ['./category-index.component.css']
})
export class CategoryIndexComponent implements OnInit {

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.getCategories(); 
  }

  public categories: CategoryDto[];
  public getCategories()
  {
    this.categoryService.getCategories().subscribe(
      (response: CategoryDto[]) =>
      {
        this.categories = response;
      }        
      );
  }

}
