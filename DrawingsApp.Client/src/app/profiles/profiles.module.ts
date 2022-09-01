import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { ProfilesRoutingModule } from './profiles-routing.module';
import { PostsModule } from '../posts/posts.module';
import { ImagesModule } from '../images/images.module';
import { EditProfileComponent } from './edit-profile/edit-profile.component';



@NgModule({
  declarations: [
    UserProfileComponent,
    EditProfileComponent
  ],
  imports: [
    CommonModule,
    ProfilesRoutingModule,
    PostsModule,
    ImagesModule
  ]
})
export class ProfilesModule { }
