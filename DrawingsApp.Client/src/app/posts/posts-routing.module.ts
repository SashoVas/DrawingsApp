import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateGroupComponent } from './create-group/create-group.component';
import { CreatePostComponent } from './create-post/create-post.component';
import { GroupComponent } from './group/group.component';
import { LandingComponent } from './landing/landing.component';

const routes: Routes = [{
  path:"",
  pathMatch:"full",
  component:LandingComponent
},
{
  path:"group",
  component:GroupComponent
},
{
  path:"group/create",
  component:CreateGroupComponent
},
{
  path:"group/create/post",
  component:CreatePostComponent
}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostsRoutingModule { }
