import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CategoryDto } from 'src/app/Models/DTOs/category-dto';
import { CategoryService } from 'src/app/Services/category.service';

interface Category {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-category-select',
  templateUrl: './category-select.component.html',
  styleUrls: ['./category-select.component.css']
})
export class CategorySelectComponent implements OnInit {
  @Input() categoryId: any;
  @Output() onCategorySelected = new EventEmitter<number>();
  constructor(categoryService: CategoryService) { 
      categoryService.getCategories().subscribe(response=> 
        {
          this.categories= response;
          console.log("categoryid is " + this.categoryId)
        }
        );
  }
  categories: CategoryDto[];
  ngOnInit(): void {

  }

  public onChange(deviceValue): void{
    this.onCategorySelected.emit(this.categoryId);
    console.log(deviceValue +"---"+ this.categoryId);
  }

  selected = 'steak-0';

  records: Category[] = [
    {value: 'steak-0', viewValue: 'Steak'},
    {value: 'pizza-1', viewValue: 'Pizza'},
    {value: 'tacos-2', viewValue: 'Tacos'},
  ];
}
