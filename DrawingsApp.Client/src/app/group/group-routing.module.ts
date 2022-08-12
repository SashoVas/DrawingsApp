import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateGroupComponent } from '../group/create-group/create-group.component';
import { GroupListingComponent } from '../group/group-listing/group-listing.component';
import { GroupComponent } from './group/group.component';
import { GroupResolver } from './resolvers/group.resolver';
import { UserListResolver } from './resolvers/user-list.resolver';
import { UsersListComponent } from './users-list/users-list.component';

const routes: Routes = [
  {
    path:"create",
    component:CreateGroupComponent
  },
  {
    path:"search",
    component:GroupListingComponent
  },
  {
    path:"users",
    component:UsersListComponent,
    resolve:{
      data:UserListResolver
    }
  },
  {
    path:":id",
    component:GroupComponent,
    resolve:{
      data:GroupResolver
    }
  }
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class GroupRoutingModule { }
  