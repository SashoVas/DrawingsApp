import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [{
  path:"",
  pathMatch:"full",
  loadChildren:()=>import("./posts/posts.module").then(h=>h.PostsModule)
},
{
  path:"images",
  loadChildren:()=>import("./images/images.module").then(i=>i.ImagesModule)
},
{
  path:"identity",
  loadChildren:()=>import("./identity/identity.module").then(i=>i.IdentityModule)
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
