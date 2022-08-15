import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IPostFull } from 'src/app/core/interfaces/IPostFull';
import { environment } from 'src/environments/environment';
import { PostService } from '../services/post.service';

@Component({
  selector: 'app-post-full',
  templateUrl: './post-full.component.html',
  styleUrls: ['./post-full.component.css']
})
export class PostFullComponent implements OnInit {
  post!:IPostFull;
  imagesUrl:string=environment.imageApi;
  current_img:number=0;
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
  changeImgRight(){
    this.current_img++;
    if(this.current_img>=this.post.imgUrls.length){
      this.current_img=0;
    }
  }
  changeImgLeft(){
    this.current_img--;
    if(this.current_img<=-1){
      this.current_img=this.post.imgUrls.length-1;
    }
  }
}
