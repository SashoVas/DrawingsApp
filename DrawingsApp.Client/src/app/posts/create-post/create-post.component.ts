import { Component, OnInit, ViewChild } from '@angular/core';
import { UserImagesComponent } from '../user-images/user-images.component';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {
  @ViewChild("images") images!:UserImagesComponent;
  constructor() { }

  ngOnInit(): void {
    
  }
  createPost(){
    console.log(this.images.getSelectedImages());
  }
}
