import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from './post/post.component';
import { LandingComponent } from './landing/landing.component';
import { PostsRoutingModule } from './posts-routing.module';
import { GroupComponent } from './group/group.component';
import { CreateGroupComponent } from './create-group/create-group.component';
import { PopularGroupsComponent } from './popular-groups/popular-groups.component';
import { CreateTagComponent } from './create-tag/create-tag.component';
import { CreatePostComponent } from './create-post/create-post.component';
import { UserImagesComponent } from './user-images/user-images.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    PostComponent,
    LandingComponent,
    GroupComponent,
    CreateGroupComponent,
    PopularGroupsComponent,
    CreateTagComponent,
    CreatePostComponent,
    UserImagesComponent
  ],
  imports: [
    CommonModule,
    PostsRoutingModule,
    ReactiveFormsModule
  ],
  exports:
  [
    PostComponent
  ]
})
export class PostsModule { }