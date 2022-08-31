import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { ProfilesRoutingModule } from './profiles-routing.module';
import { PostsModule } from '../posts/posts.module';
import { ImagesModule } from '../images/images.module';



@NgModule({
  declarations: [
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    ProfilesRoutingModule,
    PostsModule,
    ImagesModule
  ]
})
export class ProfilesModule { }
