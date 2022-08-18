import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPost } from 'src/app/core/interfaces/IPost';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http:HttpClient) { }
  getPostByUser():Observable<any>{
    return this.http.get<Array<IPost>>(environment.groupApi+"Post");
  }
  getPostByGroup(id:number):Observable<any>{
    return this.http.get<Array<IPost>>(environment.groupApi+"Post/Group/"+id);
  }

  likePost(groupId:number,postId:string,isLike:boolean){
    return this.http.put(environment.groupApi+"Post/Like",{groupId,postId,isLike});
  }
}
