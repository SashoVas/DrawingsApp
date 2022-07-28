import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private http:HttpClient) { }

  getGroup(id:string):Observable<any>{
    return this.http.get<IGroup>(environment.groupApi+"Group/"+id)
  }
  getGroupsByUser():Observable<any>{
    return this.http.get<Array<IGroup>>(environment.groupApi+"Group/User")
  }
  createGroup(title:string,moreInfo:string,imgUrl:string,groupType:number,tags:Array<number>):Observable<any>{
    return this.http.post(environment.groupApi+"Group",{title,moreInfo,imgUrl,groupType,tags});
  }
}
