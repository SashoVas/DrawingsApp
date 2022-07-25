import { Component, OnInit } from '@angular/core';
import { IImage } from 'src/app/core/interfaces/IImage';
@Component({
  selector: 'app-user-images',
  templateUrl: './user-images.component.html',
  styleUrls: ['./user-images.component.css']
})
export class UserImagesComponent implements OnInit {
  data:Array<IImage>=[
    {
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },
    {
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },
    {
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },
    {
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },
    {
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },
    {
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },{
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },
    {
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },
    {
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },
    {
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },
    {
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },
    {
      imgUrl:"../../../assets/resources/Untitled.png",
      isSelected:false
    },]
    selectedImage:IImage|null=null;
  constructor() { 
  }
  getSelectedImages(){
    return this.data.filter(i=>i.isSelected).map(i=>i.imgUrl);
  }
  ngOnInit(): void {
  }
  selectImage(imgIndex:number):void{
    this.selectedImage=this.data[imgIndex];
  }
  selectForPost():void{
    this.selectedImage!.isSelected=!this.selectedImage?.isSelected;
  }
}
