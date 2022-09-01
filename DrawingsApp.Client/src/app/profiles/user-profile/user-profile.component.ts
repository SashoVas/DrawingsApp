import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { IPost } from 'src/app/core/interfaces/IPost';
import { IProfile } from 'src/app/core/interfaces/IProfile';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  posts!:Array<IPost>;
  profile!:IProfile;
  groups!:Array<IGroup>;
  postsMode:boolean=true;
  imageApiUrl:string=environment.imageApi;
  constructor(private activatedRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.profile=this.activatedRoute.snapshot.data['data'];
    this.posts=this.activatedRoute.snapshot.data['data'].posts;
    this.groups=this.activatedRoute.snapshot.data['data'].groups;
  }
}
