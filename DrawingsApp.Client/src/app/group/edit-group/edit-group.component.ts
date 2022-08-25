import { Component, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IGroup } from 'src/app/core/interfaces/IGroup';
import { ITag } from 'src/app/core/interfaces/ITag';
import { UserImagesComponent } from 'src/app/images/user-images/user-images.component';
import { GroupService } from '../services/group.service';
import { TagsService } from '../services/tags.service';

@Component({
  selector: 'app-edit-group',
  templateUrl: './edit-group.component.html',
  styleUrls: ['./edit-group.component.css']
})
export class EditGroupComponent implements OnInit {

  editGroupForm!:UntypedFormGroup
  tags:Array<ITag>=[];
  selectedTags:Array<ITag>=[];
  groupType=0;
  group!:IGroup;
  @ViewChild("images") images!:UserImagesComponent;
  constructor(private fb:UntypedFormBuilder,private tagService:TagsService,private activatedRoute:ActivatedRoute,private groupService:GroupService,private router:Router) { 
    
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
    let images=this.images.getSelectedImages();
    let image:string=this.group.imgUrl;
    if(images.length>0){
      image=images[0];
    }
    console.log(image);
    this.groupService
    .editGroup(this.group.id,
      this.editGroupForm.value.title,
      this.editGroupForm.value.moreInfo,
      image,
      this.groupType,
      this.selectedTags.map(t=>t.tagId))
      .subscribe(()=>this.router.navigate(["/"]));
  }
}
