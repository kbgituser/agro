import { getLocaleMonthNames } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserRegistrationDto } from 'src/app/DTOs/user-registration-dto';
import { UserDto } from 'src/app/Models/user-dto';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  public userDto: UserDto = new UserDto();
  public registration: UserRegistrationDto = new UserRegistrationDto();

  errorMessage: string;
  email: string;
  password: string;
  phoneNumber: string;
  constructor(private userService: UserService, private router: Router) { 
        
  }
  ngOnInit(): void {
  }

  createUser(){
    this.registration.email = this.email;
    this.registration.password = this.password;
    this.registration.phoneNumber = this.phoneNumber;
    
    this.userService.createUser(this.registration).subscribe(response=>{
      this.router.navigate(["/"]);
    },error=> {
      this.errorMessage = error;
      console.log(this.errorMessage); 
    });
    
  }


}
