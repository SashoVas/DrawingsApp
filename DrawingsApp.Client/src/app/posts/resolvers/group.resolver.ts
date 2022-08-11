import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import {Observable } from 'rxjs';
import { GroupService } from '../services/group.service';

@Injectable({
  providedIn: 'root'
})
export class GroupResolver implements Resolve<boolean> {
  constructor(private groupService:GroupService){}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
    let id=route.paramMap.get('id');
    return this.groupService.getGroup(id!);
  }
}
