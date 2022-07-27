import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserImagesComponent } from '../user-images/user-images.component';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {
  @ViewChild("images") images!:UserImagesComponent;
  createPostForm:FormGroup;
  groups:Array<any>=[{id:0,name:"No group"},{id:1,name:"GroupName"},{id:2,name:"GroupName"},{id:3,name:"GroupName"},{id:4,name:"GroupName"},{id:5,name:"GroupName"},]
  group=this.groups[0];
  constructor(private fb:FormBuilder) {
    this.createPostForm=fb.group({
      "title":['',Validators.required],
      "explanation":[''],
    });
   }
   
  ngOnInit(): void {
    
  }
  get title(){
    return this.createPostForm.get("title");
  }
  createPost(){
    console.log(this.images.getSelectedImages());
    console.log(this.createPostForm.value)
    console.log(this.group.id)
  }
}
