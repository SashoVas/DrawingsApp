import { Component, OnInit } from '@angular/core';
import { IPost } from 'src/app/core/interfaces/IPost';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  posts:Array<IPost>=[
    { 
      id:"string",
      imgUrls:["dasf"],
      postedOn:"string",
      title:"string",
      senderUserName:"string",
      groupName:"string",
      groupId:1,
      likes:23,
      groupImgUrl:"string"
    }];
  constructor() { }

  ngOnInit(): void {
  }

}
