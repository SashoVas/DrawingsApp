import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { GroupService } from '../services/group.service';
import { PostService } from '../services/post.service';
import { UserImagesComponent } from '../user-images/user-images.component';

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
  constructor(private fb:FormBuilder,private postService:PostService,private groupService:GroupService) {
    this.createPostForm=fb.group({
      "title":['',Validators.required],
      "explanation":[''],
    });
   }
   
  ngOnInit(): void {
    this.groupService.getGroupsByUser().subscribe(groups=>{
      this.groups=groups
      this.group=groups[0]
    });
  }
  get title(){
    return this.createPostForm.get("title");
  }
  createPost(){
    this.postService.createPost(this.createPostForm.value.title,this.group.id,this.images.getSelectedImages()).subscribe();
  }
}
