import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreatePostComponent } from './create-post/create-post.component';
import { PostFullComponent } from './post-full/post-full.component';
import { PostFullResolver } from './resolvers/post-full.resolver';

const routes: Routes = [
  {
  path:"create",
  component:CreatePostComponent
},
{
  path:":id",
  component:PostFullComponent,
  resolve:{
    data:PostFullResolver
  }
},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostsRoutingModule { }
