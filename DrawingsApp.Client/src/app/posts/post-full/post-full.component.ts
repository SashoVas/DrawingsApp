import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IPostFull } from 'src/app/core/interfaces/IPostFull';
import { CommentService } from '../services/comment.service';

@Component({
  selector: 'app-post-full',
  templateUrl: './post-full.component.html',
  styleUrls: ['./post-full.component.css']
})
export class PostFullComponent implements OnInit {
  post!:IPostFull;
  constructor(private activatedRoute:ActivatedRoute,private commentService:CommentService) { }

  ngOnInit(): void {
    //this.commentService.getPostFull(this.activatedRoute.snapshot.params["id"]).subscribe(data=>this.post=data);
    this.post=this.activatedRoute.snapshot.data["data"];
  }

}
