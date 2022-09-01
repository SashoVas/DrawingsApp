import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { ProfileResolver } from './resolvers/profile.resolver';
import { UserProfileComponent } from './user-profile/user-profile.component';


const routes: Routes = [
  {
  path:"profile",
  component:UserProfileComponent,
  resolve:{data:ProfileResolver}
},
{
  path:"profile/edit",
  component:EditProfileComponent,
  resolve:{data:ProfileResolver}
},
{
  path:"profile/:id",
  component:UserProfileComponent,
  resolve:{data:ProfileResolver}
}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfilesRoutingModule { }
