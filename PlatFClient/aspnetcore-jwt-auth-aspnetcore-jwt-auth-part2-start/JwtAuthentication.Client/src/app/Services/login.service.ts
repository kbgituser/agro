import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private jwtHelper: JwtHelperService, ) { }

  public isUserAuthenticated() {
    const token : string = localStorage.getItem("jwt");    
    if (!(token === 'undefined') && token && !(this.jwtHelper.isTokenExpired(token)))
    {      
      return true;
    }
    else
    {      
      return false;
    }
  }

  public isUserAdmin(){
    return false;
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
    localStorage.removeItem("refreshToken");
  }

  public logIn(token:string , refreshToken: string) {
    localStorage.setItem("jwt", token);
    localStorage.setItem("refreshToken", refreshToken);
  }

  private roles: string[];


  setRoles(roles: string[]) {
    this.roles = roles;
  }

  hasRole(role: string) {
    this.getRoles();
    return this.roles.includes(role);
  }

  getRoles() {
    const token = localStorage.getItem('jwt');
    if (!(token === 'undefined') && token && !(this.jwtHelper.isTokenExpired(token)))
    {
      // Decode the token to get the payload
      const decodedToken = this.jwtHelper.decodeToken(token);
      console.log(decodedToken.Role);
      // Get the role information from the payload
      this.roles = decodedToken.role;
      return decodedToken.role;
    }
  }
}