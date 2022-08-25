import { Component, OnInit, ɵclearResolutionOfComponentResourcesQueue } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  searchGroupForm:UntypedFormGroup;
  constructor(private accountService:AccountService,private router:Router,private fb:UntypedFormBuilder) {
    this.searchGroupForm=fb.group({
      "groupName":[""]
    });
   }

  ngOnInit(): void {
  }
  isLogedIn():boolean{
    return this.accountService.isLogedIn();
  }
  logOut(){
    this.accountService.logOut();
  }
  search(){
    this.router.navigate(["/group/search/"],{queryParams:{name:this.searchGroupForm.value.groupName}});
  }
}
