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
  }
}
