import { Component, OnInit } from '@angular/core';
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
  data:Array<IImage>=[];
    selectedImage:IImage|null=null;
  constructor(private imageService:ImageService) { 
  }
  getSelectedImages(){
    return this.data.filter(i=>i.isSelected).map(i=>i.imgUrl);
  }
  ngOnInit(): void {
    this.imageService.getImages(null)
    .subscribe(data=>this.data=data);
  }
  selectImage(imgIndex:number):void{
    this.selectedImage=this.data[imgIndex];
  }
  selectForPost():void{
    this.selectedImage!.isSelected=!this.selectedImage?.isSelected;
  }
}
