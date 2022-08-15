import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { ImagesRoutingModule } from './images-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserImagesComponent } from './user-images/user-images.component';



@NgModule({
  declarations: [
    CreateComponent,
    UserImagesComponent
  ],
  imports: [
    CommonModule,
    ImagesRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports:[UserImagesComponent]
})
export class ImagesModule { }
