import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private http:HttpClient) { }
  getProfile(userId:string|null):Observable<any>{
    if(userId==null){
      userId="";
    }
    return this.http.get(environment.groupApi+"Profile/");
  }
}
