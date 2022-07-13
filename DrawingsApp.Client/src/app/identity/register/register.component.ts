import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm:FormGroup;  
  constructor(private fb:FormBuilder) {
    this.registerForm=this.fb.group({
      'username':['',Validators.required],
      'password':['',Validators.required],
      'confirmPassword':['',Validators.required]
    });
   }

  ngOnInit(): void {
  }
  get username(){
    return this.registerForm.get('username');
  }
  get password(){
    return this.registerForm.get('password');
  }
  get confirmPassword(){
    return this.registerForm.get('confirmPassword');
  }
}
