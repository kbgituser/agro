import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserRegistrationDto } from '../DTOs/user-registration-dto';
import { UserDto } from '../Models/user-dto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  usersUrl="https://localhost:7272/api/users"

  getUsers(){
    return this.http.get(this.usersUrl);
  }
  getUserById(id: number){
    return this.http.get(this.usersUrl+"/"+id);
  }
  createUser(userRegistrationDto: UserRegistrationDto){
    return this.http.post(this.usersUrl, userRegistrationDto);
  }
  editUser(userDto: UserDto)
  {
    return this.http.put(this.usersUrl, userDto);
  }

}
