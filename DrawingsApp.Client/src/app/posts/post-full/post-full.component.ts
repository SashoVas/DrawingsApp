import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IPostFull } from 'src/app/core/interfaces/IPostFull';
import { PostService } from '../services/post.service';

@Component({
  selector: 'app-post-full',
  templateUrl: './post-full.component.html',
  styleUrls: ['./post-full.component.css']
})
export class PostFullComponent implements OnInit {
  post!:IPostFull;
  constructor(private activatedRoute:ActivatedRoute,private postService:PostService) { }

  ngOnInit(): void {
    this.post=this.activatedRoute.snapshot.data["data"];
  }
  likePost(){
    this.postService.likePost(this.post.groupId,this.post.outerId,true).subscribe();
  }
  disLikePost(){
    this.postService.likePost(this.post.groupId,this.post.outerId,false).subscribe();
  }
}
