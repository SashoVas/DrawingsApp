import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPost } from 'src/app/core/interfaces/IPost';
import { IPostFull } from 'src/app/core/interfaces/IPostFull';
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
  likePost(groupId:number,postId:string,isLike:boolean):Observable<any>{
    return this.http.put(environment.groupApi+"Post/Like",{groupId,postId,isLike});
  }
  deletePost(postId:string):Observable<any>{
    return this.http.delete(environment.commentApi+"Post/"+postId);
  }
  getPostFull(id:string):Observable<any>{
    return this.http.get<IPostFull>(environment.commentApi+"Post/"+id);
  }
  createPost(title:string,groupId:number,description:string,imgUrls:Array<string>):Observable<any>{
    return this.http.post(environment.commentApi+"Post",{title,groupId,description,imgUrls},{responseType: 'text'});
  }
  updatePost(postId:string,title:string,description:string):Observable<any>{
    return this.http.put(environment.commentApi+"Post",{postId,title,description})
  }
}
