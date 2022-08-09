import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { CommentService } from '../services/comment.service';

@Injectable({
  providedIn: 'root'
})
export class PostFullResolver implements Resolve<boolean> {
  constructor(private commentService:CommentService){}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.commentService.getPostFull(route.params["id"]);
  }
}
