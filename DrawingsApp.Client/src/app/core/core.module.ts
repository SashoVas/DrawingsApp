import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './navigation/navigation.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    NavigationComponent
  ],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    RouterModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
  ],
  exports:[
    NavigationComponent
  ]
})
export class CoreModule { }
