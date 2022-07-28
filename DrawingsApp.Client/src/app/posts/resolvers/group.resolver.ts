import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import {  forkJoin, map, Observable, of } from 'rxjs';
import { GroupService } from '../services/group.service';
import { PostService } from '../services/post.service';

@Injectable({
  providedIn: 'root'
})
export class GroupResolver implements Resolve<boolean> {
  constructor(private groupService:GroupService,private postService:PostService){}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
    let id=route.paramMap.get('id');
    console.log(id)
    return forkJoin([this.groupService.getGroup(id!),this.postService.getPostByGroup(id!)]);
  }
}
