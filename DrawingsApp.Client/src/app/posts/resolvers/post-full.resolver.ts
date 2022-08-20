import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { CommentService } from '../services/comment.service';
import { PostService } from '../services/post.service';

@Injectable({
  providedIn: 'root'
})
export class PostFullResolver implements Resolve<boolean> {
  constructor(private postService:PostService){}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.postService.getPostFull(route.params["id"]);
  }
}
