import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, UntypedFormGroup } from '@angular/forms';
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
  editProfileForm!:UntypedFormGroup;
  @ViewChild("images") images!:UserImagesComponent;

  constructor(private profileService:ProfileService,private fb:FormBuilder) { 
  }
  ngOnInit(): void {
    this.profileService.getProfileInfo().subscribe(data=>{
      this.profile=data;
      this.editProfileForm=this.fb.group({
        "description":[data.description]
      });
    });
  }
  setImg(){
    this.profile.imgUrl=this.images.getSelectedImages()[0];
    console.log(this.profile.imgUrl);
  }
  editProfile(){
    this.profileService.editProfile(this.editProfileForm.value.description,this.profile.imgUrl).subscribe();
  }
}
