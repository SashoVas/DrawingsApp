import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateGroupComponent } from './create-group/create-group.component';
import { CreatePostComponent } from './create-post/create-post.component';
import { GroupListingComponent } from './group-listing/group-listing.component';
import { GroupComponent } from './group/group.component';
import { LandingComponent } from './landing/landing.component';
import { PostFullComponent } from './post-full/post-full.component';
import { GroupResolver } from './resolvers/group.resolver';
import { LandingResolver } from './resolvers/landing.resolver';
import { PostFullResolver } from './resolvers/post-full.resolver';
import { UsersListComponent } from './users-list/users-list.component';

const routes: Routes = [{
  path:"",
  pathMatch:"full",
  component:LandingComponent,
  resolve:{
    postsData:LandingResolver
  }
},
{
  path:"post/:id",
  component:PostFullComponent,
  resolve:{
    data:PostFullResolver
  }
},
{
  path:"group/create",
  component:CreateGroupComponent
},
{
  path:"group/search",
  component:GroupListingComponent
},
{
  path:"group/create/post",
  component:CreatePostComponent
},
{
  path:"group/:id",
  component:GroupComponent,
  resolve:{
    data:GroupResolver
  }
},
{
  path:"users/:id",
  component:UsersListComponent
}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostsRoutingModule { }
