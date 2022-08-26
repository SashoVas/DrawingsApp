import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { ITag } from 'src/app/core/interfaces/ITag';
import { GroupService } from '../services/group.service';
import { TagsService } from '../services/tags.service';
@Component({
  selector: 'app-group-listing',
  templateUrl: './group-listing.component.html',
  styleUrls: ['./group-listing.component.css']
})
export class GroupListingComponent implements OnInit {
  groups:Array<IGroup>=[];
  form!:UntypedFormGroup;
  groupType:number|null=null;
  selectedTags:Array<ITag>=[];
  order:number=0;
  tags:Array<ITag>=[];
  userId:string|null=null;
  page=0;
  constructor(private groupService:GroupService,private activatedRoute:ActivatedRoute,private fb:UntypedFormBuilder,private tagService:TagsService) {
    
   }

  ngOnInit(): void {
    this.tagService.getTags().subscribe(data=>{
      this.tags=data;
      this.activatedRoute.queryParams.subscribe(params=>{
      let name=params["name"]==undefined?"":params["name"];
      this.form=this.fb.group({
        "name":[name],
      });
      this.order=params["order"]==undefined?0:params["order"];
      this.selectedTags=params["tags"]==undefined?[]:params["tags"].filter((id:string)=>parseInt(id)).map((id:number)=>this.tags.find(t=>t.tagId==id));
      console.log(this.selectedTags);
      this.userId=params["userId"]==undefined?null:params["userId"];
      this.fetchData(name);
    });
    });
  }
  fetchData(name:string){
    this.groupService.search(name,this.userId,this.order,this.selectedTags.map(t=>t.tagId),this.page).subscribe(data=>this.groups=this.groups.concat(data));
  }
  selectTag(tag:ITag){
    tag.isSelected=!tag.isSelected;
    this.selectedTags=this.tags.filter(t=>t.isSelected);
  }
  search(){
    this.fetchData(this.form.value.name);
  }
  loadMore(){
    this.page++;
    this.fetchData(this.form.value.name);
  }
}
