import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }
  joinGroup(groupId:number):Observable<any>{
    return this.http.post(environment.groupApi+"User/"+groupId,{});
  }
  leaveGroup(groupId:number):Observable<any>{
    return this.http.delete(environment.groupApi+"User/"+groupId);
  }
}
