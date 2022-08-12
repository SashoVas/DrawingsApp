import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './group/landing/landing.component';
import { LandingResolver } from './group/resolvers/landing.resolver';


const routes: Routes = [{
  path:"",
  pathMatch:"full",
  component:LandingComponent,
  resolve:{
    postsData:LandingResolver
  }
},
{
  path:"group",
  loadChildren:()=>import("./group/group.module").then(g=>g.GroupModule)
},
{
  path:"post",
  loadChildren:()=>import("./posts/posts.module").then(p=>p.PostsModule)
},
{
  path:"images",
  loadChildren:()=>import("./images/images.module").then(i=>i.ImagesModule)
},
{
  path:"identity",
  loadChildren:()=>import("./identity/identity.module").then(i=>i.IdentityModule)
},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
