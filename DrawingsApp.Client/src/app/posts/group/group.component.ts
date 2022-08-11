import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { IPost } from 'src/app/core/interfaces/IPost';
import { PostService } from '../services/post.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {

  constructor(private activatedRoute:ActivatedRoute,private userService:UserService,private postService:PostService) { }
  group!:IGroup;
  posts:Array<IPost>=[];
  ngOnInit(): void {
    console.log();
    this.group=this.activatedRoute.snapshot.data['data'];
    if(this.group.groupType==0 || this.group.role>=2)
    {
      this.postService.getPostByGroup(this.group.id).subscribe(data=>this.posts=data);
    }
    
  }
  join(){
    this.userService.joinGroup(this.group.id).subscribe(()=>this.group.role=this.group.groupType<2?2:1);
  }
  leave(){
    this.userService.leaveGroup(this.group.id).subscribe(()=>this.group.role=0);
    
  }
}
