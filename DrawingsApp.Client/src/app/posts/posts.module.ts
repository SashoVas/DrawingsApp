import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from './post/post.component';
import { PostsRoutingModule } from './posts-routing.module';
import { CreatePostComponent } from './create-post/create-post.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PostFullComponent } from './post-full/post-full.component';
import { CommentComponent } from './comment/comment.component';
import { CommentListComponent } from './comment-list/comment-list.component';
import { ImagesModule } from '../images/images.module';

@NgModule({
  declarations: [
    PostComponent,
    CreatePostComponent,
    PostFullComponent,
    CommentComponent,
    CommentListComponent,
  ],
  imports: [
    CommonModule,
    PostsRoutingModule,
    ReactiveFormsModule,
    ImagesModule
  ],
  exports:
  [
    PostComponent
  ]
})
export class PostsModule { }