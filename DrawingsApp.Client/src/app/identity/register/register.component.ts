import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IdentityService } from '../services/identity.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm:FormGroup;  
  constructor(private fb:FormBuilder,private identityService:IdentityService,private router:Router) {
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
  submit(){
    this.identityService.register(this.registerForm.value.username,this.registerForm.value.password,this.registerForm.value.confirmPassword).subscribe(()=>this.router.navigate(["identity/login"],))
  }
}
