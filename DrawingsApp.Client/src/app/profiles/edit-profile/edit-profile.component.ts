import { Component, OnInit, ViewChild } from '@angular/core';
import { IProfile } from 'src/app/core/interfaces/IProfile';
import { UserImagesComponent } from 'src/app/images/user-images/user-images.component';
import { environment } from 'src/environments/environment';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {
  profile!:IProfile;
  imageApiUrl:string=environment.imageApi;
  @ViewChild("images") images!:UserImagesComponent;

  constructor(private profileService:ProfileService) { }
  ngOnInit(): void {
    this.profileService.getProfileInfo().subscribe(data=>this.profile=data);
  }
  setImg(){
    this.profile.imgUrl=this.images.getSelectedImages()[0];
    console.log(this.profile.imgUrl);
  }
}
