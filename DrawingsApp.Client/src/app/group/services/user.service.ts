import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/core/interfaces/IUser';
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
  getUsers(groupId:number,role:number):Observable<any>{
    return this.http.get<Array<IUser>>(environment.groupApi+"User/"+groupId+"?role="+role);
  }
  getRole(groupId:number):Observable<any>{
    return this.http.get<number>(environment.groupApi+"User/Role/"+groupId);
  }
}
