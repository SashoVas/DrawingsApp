import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from './post/post.component';
import { LandingComponent } from './landing/landing.component';
import { PostsRoutingModule } from './posts-routing.module';
import { GroupComponent } from './group/group.component';
import { CreateGroupComponent } from './create-group/create-group.component';

@NgModule({
  declarations: [
    PostComponent,
    LandingComponent,
    GroupComponent,
    CreateGroupComponent
  ],
  imports: [
    CommonModule,
    PostsRoutingModule
  ],
  exports:
  [
    PostComponent
  ]
})
export class PostsModule { }