import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IComment } from 'src/app/core/interfaces/IComment';
import { CommentService } from '../services/comment.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input()comment!:IComment;
  replyMode:boolean=false;
  replyForm!:FormGroup;
  @Input()commentPath:Array<string>=[];
  constructor(private fb:FormBuilder,private commentService:CommentService) { }

  ngOnInit(): void {
    this.replyForm=this.fb.group({
      "content":["",Validators.required]
    });
  }
  reply(){
    console.log(this.replyForm.value);
    console.log(this.commentPath.concat(this.comment.id));
    this.commentService.replyOnComment(this.replyForm.value.content,this.comment.postId,this.commentPath.concat(this.comment.id))
    .subscribe(new_comment=>this.comment.comments.push(new_comment));
    this.replyMode=false;

  }
}