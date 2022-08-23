import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateGroupComponent } from './create-group/create-group.component';
import { GroupComponent } from './group/group.component';
import { GroupListingComponent } from './group-listing/group-listing.component';
import { PopularGroupsComponent } from './popular-groups/popular-groups.component';
import { PostsModule } from '../posts/posts.module';
import { ReactiveFormsModule } from '@angular/forms';
import { GroupRoutingModule } from './group-routing.module';
import { LandingComponent } from './landing/landing.component';
import { CreateTagComponent } from './create-tag/create-tag.component';
import { UsersListComponent } from './users-list/users-list.component';
import { EditGroupComponent } from './edit-group/edit-group.component';
import { ImagesModule } from '../images/images.module';



@NgModule({
  declarations: [
    CreateGroupComponent,
    GroupComponent,
    GroupListingComponent,
    PopularGroupsComponent,
    LandingComponent,
    CreateTagComponent,
    UsersListComponent,
    EditGroupComponent
  ],
  imports: [
    CommonModule,
    GroupRoutingModule,
    PostsModule,
    ReactiveFormsModule,
    ImagesModule
    ],
  exports:[PopularGroupsComponent]
})
export class GroupModule { }
