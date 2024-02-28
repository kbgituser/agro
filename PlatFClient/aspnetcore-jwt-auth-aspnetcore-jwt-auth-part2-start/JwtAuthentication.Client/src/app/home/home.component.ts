import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginService } from '../Services/login.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private jwtHelper: JwtHelperService,private loginService: LoginService) { 
    this.showRoles();

  }

  ngOnInit(): void {
    
  }

  showRoles(){
    const roles = this.loginService.getRoles();
    console.log(roles);
  }

  isUserAuthenticated() {
    // const token : string = localStorage.getItem("jwt");    
    // if (!(token === 'undefined') && token && !(this.jwtHelper.isTokenExpired(token)))
    // {      
    //   return true;
    // }
    // else
    // {      
    //   return false;
    // }
    return this.loginService.isUserAuthenticated();
  }

  logOut = () => {
        this.loginService.logOut();    
  }

}
