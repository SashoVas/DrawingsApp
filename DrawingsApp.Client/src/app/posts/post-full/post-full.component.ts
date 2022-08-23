import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IPostFull } from 'src/app/core/interfaces/IPostFull';
import { environment } from 'src/environments/environment';
import { CommentService } from '../services/comment.service';
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
  commentForm!:FormGroup;
  editMode:boolean=false;
  @ViewChild("titleEdit")title!:ElementRef;
  constructor(private activatedRoute:ActivatedRoute,private postService:PostService,private fb:FormBuilder,private router:Router,private commentService:CommentService) { }

  ngOnInit(): void {
    this.post=this.activatedRoute.snapshot.data["data"];
    this.commentForm=this.fb.group({
      "content":["",Validators.required]
    });
  }
  likePost(){
    this.postService.likePost(this.post.group.groupId,this.post.id,true).subscribe(changeAmounth=>this.post.likes+=changeAmounth);
  }
  disLikePost(){
    this.postService.likePost(this.post.group.groupId,this.post.id,false).subscribe(changeAmounth=>this.post.likes+=changeAmounth);
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
  deletePost(){
    this.postService.deletePost(this.post.id).subscribe(()=>this.router.navigate(["/"]));
  }
  editPost(){
    let newTitle:string=this.title.nativeElement.value;
    if(newTitle!=this.post.title){
      this.postService.updatePost(this.post.id,newTitle,"").subscribe(()=>{
        this.post.title=newTitle
        this.editMode=!this.editMode;
      });
    }
  }
  comment(){
    this.commentService.commentPost(this.commentForm.value.content,this.post.id).subscribe(comment=>this.post.comments.push(comment));
  }
}
