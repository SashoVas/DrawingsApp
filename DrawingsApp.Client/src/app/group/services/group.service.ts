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
  getGroupsByUser(isLess:boolean=false):Observable<any>{
    let url=environment.groupApi+"Group/User";
    if(isLess){
      url=url+"?isLess=true"
    }
    return this.http.get<Array<IGroup>>(url)
  }
  createGroup(title:string,moreInfo:string,imgUrl:string,groupType:number,tags:Array<number>):Observable<any>{
    return this.http.post(environment.groupApi+"Group",{title,moreInfo,imgUrl,groupType,tags});
  }
  getTopGroups(isLess:boolean=false):Observable<any>{
    let url=environment.groupApi+"Group/Top";
    if(isLess){
      url=url+"?isLess=true"
    }
    return this.http.get<Array<IGroup>>(url);
  }
  search(name:string,userId:string|null,order:number,tags:Array<number>,page:number):Observable<any>{
    let s:string=userId!=null?userId:"";
    return this.http.get<Array<IGroup>>(environment.groupApi+"Group",{params:{name,order,tags,page,userId:s}});
  }
  editGroup(groupId:number,title:string,moreInfo:string,imgUrl:string,groupType:number,tags:Array<number>){
    return this.http.put(environment.groupApi+"Group",{groupId,title,moreInfo,imgUrl,groupType,tags})
  }
}
