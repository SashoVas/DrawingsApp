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

const routes: Routes = [{
  path:"",
  pathMatch:"full",
  component:LandingComponent,
  resolve:{
    postsData:LandingResolver
  }
},
{
  path:"post",
  component:PostFullComponent
},
{
  path:"group/create",
  component:CreateGroupComponent
},
{
  path:"group/search/:name",
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
}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostsRoutingModule { }
