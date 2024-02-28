import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {
  customers: any;
  constructor (private http: HttpClient) { }
  

  ngOnInit(): void {
                   //https://localhost:7272/api/Customers
    this.http.get("https://localhost:7272/api/Customers")
    .subscribe( response => 
      {
        this.customers = response        
      }, (error: HttpErrorResponse) => 
      {

        console.log(error);
      })
  }

}
