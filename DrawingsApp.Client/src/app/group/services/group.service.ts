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
  getTopGroups():Observable<any>{
    return this.http.get<Array<IGroup>>(environment.groupApi+"Group/Top");
  }
  getGroupsByName(name:string,userId:string|null,order:number,tags:Array<number>):Observable<any>{
    let tagsString="";
    for(let i=0;i<tags.length;i++){
      tagsString+="&tags["+i+"]="+tags[i];
    }
    return this.http.get<Array<IGroup>>(environment.groupApi+"Group?name="+name+"&userId="+(userId!=null?userId:"")+"&order="+order+tagsString);
  }
}
