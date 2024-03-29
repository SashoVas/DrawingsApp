import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { TagsService } from '../services/tags.service';

@Component({
  selector: 'app-create-tag',
  templateUrl: './create-tag.component.html',
  styleUrls: ['./create-tag.component.css']
})
export class CreateTagComponent implements OnInit {
  createTagForm:UntypedFormGroup;
  constructor(private fb:UntypedFormBuilder,private tagService:TagsService) { 
    this.createTagForm=fb.group({
      "name":['',Validators.required],
      "tagInfo":['',Validators.required]
    })
  }
  get name(){
    return this.createTagForm.get("name");
  }
  get tagInfo(){
    return this.createTagForm.get("tagInfo");
  }
  ngOnInit(): void {
  }
  createTag(){
    this.tagService.createTag(this.createTagForm.value.name,this.createTagForm.value.tagInfo).subscribe();
  }
}
