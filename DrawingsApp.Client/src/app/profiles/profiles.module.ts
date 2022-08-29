import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { ProfilesRoutingModule } from './profiles-routing.module';
import { PostsModule } from '../posts/posts.module';



@NgModule({
  declarations: [
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    ProfilesRoutingModule,
    PostsModule
  ]
})
export class ProfilesModule { }
