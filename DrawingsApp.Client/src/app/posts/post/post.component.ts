import { Component, Input, OnInit } from '@angular/core';
import { IPost } from 'src/app/core/interfaces/IPost';
import { environment } from 'src/environments/environment';
import { PostService } from '../services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  @Input()post!:IPost;
  constructor(private postService:PostService) { }
  imagesUrl:string=environment.imageApi;
  ngOnInit(): void {
  }
  likePost(){
    this.postService.likePost(this.post.groupId,this.post.id,true).subscribe();
  }
  disLikePost(){
    this.postService.likePost(this.post.groupId,this.post.id,false).subscribe();
  }
}
