import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ITag } from 'src/app/core/interfaces/ITag';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.css']
})
export class CreateGroupComponent implements OnInit {
  createGroupForm:FormGroup
  tags:Array<ITag>=[{id:1,name:"Sport",isSelected:false},{id:2,name:"News",isSelected:false},{id:3,name:"Gaming",isSelected:false},];
  groupType=1;
  constructor(private fb:FormBuilder) {
    this.createGroupForm=fb.group({
      "title":['',Validators.required],
      "moreInfo":['']
    });
   }

  ngOnInit(): void {
  }
  get title(){
    return this.createGroupForm.get("title");
  }
  selectTag(tagIndex:number){
    this.tags[tagIndex].isSelected=!this.tags[tagIndex].isSelected;
  }
  createGroup(){
    console.log(this.createGroupForm.value);
    console.log(this.tags.filter(t=>t.isSelected).map(t=>t.id))
    console.log(this.groupType);
  }
}
