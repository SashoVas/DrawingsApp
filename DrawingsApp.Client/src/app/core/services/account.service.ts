import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor() { }
  isLogedIn():boolean{
    return localStorage.getItem("token")!=undefined;
  }
  setToken(token:string){
    localStorage.setItem("token",token);
  }
  getToken():string|null{
    return localStorage.getItem("token");
  }
  logOut(){
    localStorage.removeItem("token");
  }
}
