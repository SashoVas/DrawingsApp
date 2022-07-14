import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/core/services/account.service';
import { IdentityService } from '../services/identity.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm:FormGroup;  
  constructor(private fb:FormBuilder,private identityService:IdentityService,private accountService:AccountService,private router:Router) {
    this.loginForm=this.fb.group({
      'username':['',Validators.required],
      'password':['',Validators.required]
    });
   }

  ngOnInit(): void {
  }
  get username(){
    return this.loginForm.get('username');
  }
  get password(){
    return this.loginForm.get('password');
  }
  submit(){
    this.identityService.login(this.loginForm.value.username,this.loginForm.value.password).subscribe(data=>{
      this.accountService.setToken(data.token);
      this.router.navigate([""]);
    })
  }
}
