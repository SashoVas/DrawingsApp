import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { ImagesRoutingModule } from './images-routing.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    CreateComponent
  ],
  imports: [
    CommonModule,
    ImagesRoutingModule,
    FormsModule
  ]
})
export class ImagesModule { }
