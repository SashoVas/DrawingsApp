import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { GroupService } from '../services/group.service';

@Component({
  selector: 'app-group-listing',
  templateUrl: './group-listing.component.html',
  styleUrls: ['./group-listing.component.css']
})
export class GroupListingComponent implements OnInit {
  groups:Array<IGroup>=[];
  form!:FormGroup;
  constructor(private groupService:GroupService,private activatedRoute:ActivatedRoute,private fb:FormBuilder ) {
    
   }

  ngOnInit(): void {
    this.fetchData(this.activatedRoute.snapshot.params["name"]);
    this.form=this.fb.group({
      "groupName":[this.activatedRoute.snapshot.params["name"]]
    });
  }
  fetchData(name:string){
    this.groupService.getGroupsByName(name).subscribe(data=>this.groups=data);
  }
  search(){
    console.log(this.form.value.groupName);
    this.fetchData(this.form.value.groupName);
  }
}
