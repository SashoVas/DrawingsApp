import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { ITag } from 'src/app/core/interfaces/ITag';
import { GroupService } from '../services/group.service';
import { TagsService } from '../services/tags.service';

@Component({
  selector: 'app-edit-group',
  templateUrl: './edit-group.component.html',
  styleUrls: ['./edit-group.component.css']
})
export class EditGroupComponent implements OnInit {

  editGroupForm!:FormGroup
  tags:Array<ITag>=[];
  selectedTags:Array<ITag>=[];
  groupType=0;
  group!:IGroup;
  constructor(private fb:FormBuilder,private tagService:TagsService,private activatedRoute:ActivatedRoute,private groupService:GroupService) { 
    
  }

  ngOnInit(): void {
    this.group=this.activatedRoute.snapshot.data['data'];
    this.editGroupForm=this.fb.group({
      "title":[this.group.title,Validators.required],
      "moreInfo":[this.group.moreInfo]
    });
    this.groupType=this.group.groupType;
    console.log(this.groupType);
    this.tagService.getTags().subscribe(data=>{
      this.tags=data
      this.tags=this.tags.filter(t=>!this.group.tags.includes(t.tagName));
    });
    
  }
  get title(){
    return this.editGroupForm.get("title");
  }
  selectTag(tag:ITag){
    tag.isSelected=!tag.isSelected;
    this.selectedTags=this.tags.filter(t=>t.isSelected);
  }
  editGroup(){
    this.groupService.editGroup(this.group.id,this.editGroupForm.value.title,this.editGroupForm.value.moreInfo,this.group.imgUrl,this.groupType,this.selectedTags.map(t=>t.tagId)).subscribe();
  }
}
