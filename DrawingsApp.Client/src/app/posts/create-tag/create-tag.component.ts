import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-tag',
  templateUrl: './create-tag.component.html',
  styleUrls: ['./create-tag.component.css']
})
export class CreateTagComponent implements OnInit {
  createTagForm:FormGroup;
  constructor(private fb:FormBuilder) { 
    this.createTagForm=fb.group({
      "name":['',Validators.required]
    })
  }
  get name(){
    return this.createTagForm.get("name");
  }
  ngOnInit(): void {
  }
  createTag(){
    console.log(this.createTagForm.value)
  }
}
