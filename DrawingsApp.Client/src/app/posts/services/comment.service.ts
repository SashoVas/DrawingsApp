import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPostFull } from 'src/app/core/interfaces/IPostFull';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private http:HttpClient) { }
  getPostFull(id:string):Observable<any>{
    return this.http.get<IPostFull>(environment.commentApi+"Post/"+id);
  }
  createPost(title:string,groupId:number,description:string,imgUrls:Array<string>):Observable<any>{
    return this.http.post(environment.commentApi+"Post",{title,groupId,description,imgUrls});
  }
}
