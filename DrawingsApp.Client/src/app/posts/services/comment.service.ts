import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private http:HttpClient) { }

  commentPost(contents:string,postId:string):Observable<any>{
    return this.http.post(environment.commentApi+"Comments/Post",{contents,postId});
  }
  replyOnComment(contents:string,postId:string,commentsPath:Array<string>):Observable<any>{
    return this.http.post(environment.commentApi+"Comments/Comment",{contents,postId,commentsPath});
  } 
}
