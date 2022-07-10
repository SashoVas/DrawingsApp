import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from './post/post.component';
import { LandingComponent } from './landing/landing.component';
import { PostsRoutingModule } from './posts-routing.module';

@NgModule({
  declarations: [
    PostComponent,
    LandingComponent
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