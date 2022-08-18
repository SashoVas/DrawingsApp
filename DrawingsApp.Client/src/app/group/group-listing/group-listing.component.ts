import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { ITag } from 'src/app/core/interfaces/ITag';
import { GroupService } from '../services/group.service';
import { TagsService } from '../services/tags.service';
import { UserService } from '../services/user.service';
@Component({
  selector: 'app-group-listing',
  templateUrl: './group-listing.component.html',
  styleUrls: ['./group-listing.component.css']
})
export class GroupListingComponent implements OnInit {
  groups:Array<IGroup>=[];
  form!:FormGroup;
  groupType:number|null=null;
  selectedTags:Array<ITag>=[];
  order:number=0;
  tags:Array<ITag>=[];
  userId:string|null=null;
  constructor(private groupService:GroupService,private activatedRoute:ActivatedRoute,private fb:FormBuilder,private tagService:TagsService) {
    
   }

  ngOnInit(): void {
    
    this.activatedRoute.queryParams.subscribe(params=>{
      console.log(params)
      let name=params["name"]==undefined?"":params["name"];
      this.form=this.fb.group({
        "name":name,
      });
      this.order=params["order"]==undefined?0:params["order"];
      this.selectedTags=params["tags"]==undefined?[]:params["tags"];
      this.userId=params["userId"]==undefined?null:params["userId"];
      console.log(this.order,this.selectedTags);
      this.fetchData(name);
    });
    this.tagService.getTags().subscribe(data=>this.tags=data);
  }
  fetchData(name:string){
    this.groupService.search(name,this.userId,this.order,this.selectedTags.map(t=>t.tagId),0).subscribe(data=>this.groups=data);
  }
  selectTag(tag:ITag){
    tag.isSelected=!tag.isSelected;
    this.selectedTags=this.tags.filter(t=>t.isSelected);
  }
  search(){
    console.log(this.form.value.name);
    this.fetchData(this.form.value.name);
  }
}
