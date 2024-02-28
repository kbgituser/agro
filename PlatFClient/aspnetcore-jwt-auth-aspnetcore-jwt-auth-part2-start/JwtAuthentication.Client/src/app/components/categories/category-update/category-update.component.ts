import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryDto } from 'src/app/Models/DTOs/category-dto';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-category-update',
  templateUrl: './category-update.component.html',
  styleUrls: ['./category-update.component.css']
})
export class CategoryUpdateComponent implements OnInit {
categoryDto: CategoryDto = new CategoryDto();
categoryId: string;
create: boolean = false ;
public categories: CategoryDto[] = [];
  constructor( private categroryService: CategoryService, private router: Router, private route: ActivatedRoute) { 
    this.categoryId = this.route.snapshot.paramMap.get('id');
    if (this.categoryId)
    {
      categroryService.getCategoryById(this.categoryId).subscribe(response => 
        {
          this.categoryDto = response;        
        });
      categroryService.getCategories().subscribe(response => {
        this.categories = response
      });
    }
    else
    {
        this.create= true;
    }    
  }

  ngOnInit(): void {
    
  }

  UpdateCategory(){
    if(this.create)
    {
      //this.categroryService.getCategories
    }
    else
    {
      this.categroryService.updateCategory(this.categoryDto).subscribe(
        Response=> this.router.navigate(['/categories']), 
        (err) => {console.log(err)}
      );
    }
    
  }

  onChange(categoryId:any) {
    this.categoryDto.parentCategoryId = categoryId;
  }

}
