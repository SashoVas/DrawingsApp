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
import { PostFullComponent } from './post-full/post-full.component';
import { CommentComponent } from './comment/comment.component';
import { CommentListComponent } from './comment-list/comment-list.component';
import { GroupListingComponent } from './group-listing/group-listing.component';
import { UsersListComponent } from './users-list/users-list.component';

@NgModule({
  declarations: [
    PostComponent,
    LandingComponent,
    GroupComponent,
    CreateGroupComponent,
    PopularGroupsComponent,
    CreateTagComponent,
    CreatePostComponent,
    UserImagesComponent,
    PostFullComponent,
    CommentComponent,
    CommentListComponent,
    GroupListingComponent,
    UsersListComponent
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