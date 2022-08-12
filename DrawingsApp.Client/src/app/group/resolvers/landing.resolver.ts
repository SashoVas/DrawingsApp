import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { PostService } from 'src/app/posts/services/post.service';

@Injectable({
  providedIn: 'root'
})
export class LandingResolver implements Resolve<boolean> {
  constructor(private postService:PostService){}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.postService.getPostByUser();
  }
}
