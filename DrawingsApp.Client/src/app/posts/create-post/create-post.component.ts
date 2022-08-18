import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute,Router } from '@angular/router';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { GroupService } from 'src/app/group/services/group.service';
import { UserImagesComponent } from '../../images/user-images/user-images.component';
import { CommentService } from '../services/comment.service';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {
  @ViewChild("images") images!:UserImagesComponent;
  createPostForm:FormGroup;
  groups:Array<IGroup>=[];
  group!:IGroup;
  groupId!:number;
  constructor(private fb:FormBuilder,private commentService:CommentService,private groupService:GroupService,private router:Router,private activatedRoute:ActivatedRoute) {
    this.createPostForm=fb.group({
      "title":['',Validators.required],
      "description":[''],
    });
   }
   
  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params=>
      this.groupService.getGroupsByUser().subscribe(groups=>{
      this.groups=groups;
      console.log(groups);
      if(params["id"]){
        this.group=this.groups.find(gr=>gr.id==params["id"])!;
      }
      else{
        this.group=this.groups[0];
      }
    }));
    
  }
  get title(){
    return this.createPostForm.get("title");
  }
  createPost(){
    this.commentService.createPost(this.createPostForm.value.title,this.group.id,this.createPostForm.value.description,this.images.getSelectedImages())
    .subscribe(r=>this.router.navigate(["/"]));
  }
}
