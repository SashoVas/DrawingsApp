import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private http:HttpClient) { }

  register(username:string,password:string,confirmPassword:string):Observable<any>{
    return this.http.post(environment.identityApi+"Identity/Register",{username,password,confirmPassword})
  }
  login(username:string,password:string):Observable<any>{
    return this.http.post<string>(environment.identityApi+"Identity/Login",{username,password})
  }
}
