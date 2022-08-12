import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from './post/post.component';
import { PostsRoutingModule } from './posts-routing.module';
import { CreatePostComponent } from './create-post/create-post.component';
import { UserImagesComponent } from './user-images/user-images.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PostFullComponent } from './post-full/post-full.component';
import { CommentComponent } from './comment/comment.component';
import { CommentListComponent } from './comment-list/comment-list.component';

@NgModule({
  declarations: [
    PostComponent,
    CreatePostComponent,
    UserImagesComponent,
    PostFullComponent,
    CommentComponent,
    CommentListComponent,
  ],
  imports: [
    CommonModule,
    PostsRoutingModule,
    ReactiveFormsModule,
  ],
  exports:
  [
    PostComponent
  ]
})
export class PostsModule { }