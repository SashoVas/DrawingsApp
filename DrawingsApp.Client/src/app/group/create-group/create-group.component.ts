import { Component, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ITag } from 'src/app/core/interfaces/ITag';
import { UserImagesComponent } from 'src/app/images/user-images/user-images.component';
import { GroupService } from '../services/group.service';
import { TagsService } from '../services/tags.service';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.css']
})
export class CreateGroupComponent implements OnInit {
  createGroupForm:UntypedFormGroup
  tags:Array<ITag>=[];
  selectedTags:Array<ITag>=[];
  groupType=1;
  @ViewChild("images") images!:UserImagesComponent;

  constructor(private fb:UntypedFormBuilder,private groupService:GroupService,private tagService:TagsService,private router:Router) {
    this.createGroupForm=fb.group({
      "title":['',Validators.required],
      "moreInfo":['']
    });
   }

  ngOnInit(): void {
    this.tagService.getTags().subscribe(data=>this.tags=data);
  }
  get title(){
    return this.createGroupForm.get("title");
  }
  selectTag(tag:ITag){
    tag.isSelected=!tag.isSelected;
    this.selectedTags=this.tags.filter(t=>t.isSelected);
  }
  createGroup(){
    this.groupService.createGroup(this.createGroupForm.value.title,
      this.createGroupForm.value.moreInfo,
      this.images.getSelectedImages()[0],this.groupType,this.tags.filter(t=>t.isSelected).map(t=>t.tagId))
    .subscribe(r=>this.router.navigate(["/"]));
  }
}
