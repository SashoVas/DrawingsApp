import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { IImage } from 'src/app/core/interfaces/IImage';
import { environment } from 'src/environments/environment';
import { ImageService } from '../services/image.service';
@Component({
  selector: 'app-user-images',
  templateUrl: './user-images.component.html',
  styleUrls: ['./user-images.component.css']
})
export class UserImagesComponent implements OnInit {
  imagesUrl:string=environment.imageApi;
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
  constructor(private imageService:ImageService) { 
  }
  getSelectedImages(){
    return this.data.filter(i=>i.isSelected).map(i=>i.imgUrl);
  }
  ngOnInit(): void {
    this.imageService.getImages(null).pipe(map((x)=>x.map((img:string)=>{return{imgUrl:img,isSelected:false}})))
    .subscribe(data=>{
      console.log(data);
      this.data=data});
  }
  selectImage(imgIndex:number):void{
    this.selectedImage=this.data[imgIndex];
  }
  selectForPost():void{
    this.selectedImage!.isSelected=!this.selectedImage?.isSelected;
  }
}
