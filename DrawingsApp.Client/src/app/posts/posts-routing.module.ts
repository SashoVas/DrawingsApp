import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateGroupComponent } from './create-group/create-group.component';
import { CreatePostComponent } from './create-post/create-post.component';
import { GroupComponent } from './group/group.component';
import { LandingComponent } from './landing/landing.component';
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
  path:"group/create",
  component:CreateGroupComponent
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
