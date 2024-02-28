import { Component, OnInit } from '@angular/core';
import { RequestDto } from 'src/app/Models/Request/request-dto';
import { RequestService } from 'src/app/Services/request.service';

@Component({
  selector: 'app-request-index',
  templateUrl: './request-index.component.html',
  styleUrls: ['./request-index.component.css']
})
export class RequestIndexComponent implements OnInit {

  constructor(private requestService: RequestService) { 
    
  }
  public requests: RequestDto[] = [];

  ngOnInit(): void {
    this.getRequestsByPage();
  }

  getRequestsByPage(page = 1)
  {
    this.requestService.getRequestsByPage(page).subscribe(
      response =>
      {
        this.requests = response;
        console.log(this.requests);

      }
    )
  }

}
